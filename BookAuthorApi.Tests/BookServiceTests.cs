using BookAuthorApi.DTOs;
using BookAuthorApi.Models;
using BookAuthorApi.Repositories;
using BookAuthorApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookAuthorApi.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldCreateBook_WhenValid()
        {
            var bookRepoMock = new Mock<IBookRepository>();
            var authorRepoMock = new Mock<IAuthorRepository>();
            var openLibraryMock = new Mock<BookAuthorApi.External.OpenLibraryService>(null!);

            authorRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Author { Id = Guid.NewGuid(), Name = "Test" });
            openLibraryMock.Setup(s => s.GetCoverUrlAsync(It.IsAny<string>())).ReturnsAsync("http://cover.url");
            bookRepoMock.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync((Book b) => b);

            var service = new BookService(bookRepoMock.Object, authorRepoMock.Object, openLibraryMock.Object);
            var dto = new BookDto { Isbn = "1234567890123", Title = "Titulo", PublicationYear = 2020, AuthorId = Guid.NewGuid() };
            var result = await service.AddAsync(dto);

            Assert.NotNull(result.Id);
            Assert.Equal("TITULO", result.Title); // Normalizado
            Assert.Equal("http://cover.url", result.CoverUrl);
        }
    }
}
