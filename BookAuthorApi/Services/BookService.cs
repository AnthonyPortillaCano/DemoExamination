using BookAuthorApi.DTOs;
using BookAuthorApi.Models;
using BookAuthorApi.Repositories;
using BookAuthorApi.Helpers;
using BookAuthorApi.External;
// ...existing code...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthorApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly OpenLibraryService _openLibraryService;
        public BookService(IBookRepository bookRepo, IAuthorRepository authorRepo, OpenLibraryService openLibraryService)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _openLibraryService = openLibraryService;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(string? title = null, string? authorName = null, int page = 1, int pageSize = 10)
        {
            var books = await _bookRepo.GetAllAsync(title, authorName, page, pageSize);
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                CoverUrl = b.CoverUrl,
                PublicationYear = b.PublicationYear,
                AuthorId = b.AuthorId
            });
        }

        public async Task<BookDto?> GetByIdAsync(Guid id)
        {
            var b = await _bookRepo.GetByIdAsync(id);
            if (b == null) return null;
            return new BookDto
            {
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                CoverUrl = b.CoverUrl,
                PublicationYear = b.PublicationYear,
                AuthorId = b.AuthorId
            };
        }

        public async Task<BookDto> AddAsync(BookDto bookDto)
        {
            // Normalizar título
            bookDto.Title = StringNormalizer.Normalize(bookDto.Title);
            // Validar ISBN (stub local)
            if (!IsValidIsbn(bookDto.Isbn)) throw new Exception("Invalid ISBN");
            // Obtener CoverUrl vía REST
            bookDto.CoverUrl = await _openLibraryService.GetCoverUrlAsync(bookDto.Isbn) ?? string.Empty;
            var author = await _authorRepo.GetByIdAsync(bookDto.AuthorId);
            if (author == null) throw new Exception("Author not found");
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Isbn = bookDto.Isbn,
                Title = bookDto.Title,
                CoverUrl = bookDto.CoverUrl,
                PublicationYear = bookDto.PublicationYear,
                AuthorId = bookDto.AuthorId
            };
            await _bookRepo.AddAsync(book);
            bookDto.Id = book.Id;
            return bookDto;
        }

        public async Task<BookDto?> UpdateAsync(Guid id, BookDto bookDto)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return null;
            // Normalizar título
            bookDto.Title = StringNormalizer.Normalize(bookDto.Title);
            // Validar ISBN (stub local)
            if (!IsValidIsbn(bookDto.Isbn)) throw new Exception("Invalid ISBN");
            // Obtener CoverUrl vía REST
            bookDto.CoverUrl = await _openLibraryService.GetCoverUrlAsync(bookDto.Isbn) ?? string.Empty;
            book.Isbn = bookDto.Isbn;
            book.Title = bookDto.Title;
            book.CoverUrl = bookDto.CoverUrl;
            book.PublicationYear = bookDto.PublicationYear;
            book.AuthorId = bookDto.AuthorId;
            await _bookRepo.UpdateAsync(book);
            return bookDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _bookRepo.DeleteAsync(id);
        }

        public async Task<BookCsvUploadResultDto> UploadCsvAsync(System.IO.Stream csvStream)
        {
            var result = new BookCsvUploadResultDto();
            var rows = CsvHelper.ParseCsv(csvStream).ToList();
            result.Total = rows.Count;
            foreach (var row in rows)
            {
                try
                {
                    var authorName = StringNormalizer.Normalize(row["authorName"]);
                    var author = await _authorRepo.GetByNameAsync(authorName);
                    if (author == null)
                    {
                        author = new Author { Id = Guid.NewGuid(), Name = authorName };
                        await _authorRepo.AddAsync(author);
                    }
                    var bookDto = new BookDto
                    {
                        Isbn = row["isbn"],
                        Title = row["title"],
                        PublicationYear = int.TryParse(row["publicationYear"], out var py) ? py : 0,
                        AuthorId = author.Id
                    };
                    await AddAsync(bookDto);
                    result.Success++;
                }
                catch (Exception ex)
                {
                    result.Failed++;
                    result.Errors.Add(ex.Message);
                }
            }
            return result;
        }
        private bool IsValidIsbn(string isbn)
        {
            // Solo valida longitud 13 y dígitos (stub)
            return !string.IsNullOrWhiteSpace(isbn) && isbn.Length == 13 && isbn.All(char.IsDigit);
        }
    }
}
