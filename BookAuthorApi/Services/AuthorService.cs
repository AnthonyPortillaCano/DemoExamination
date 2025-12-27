using BookAuthorApi.DTOs;
using BookAuthorApi.Models;
using BookAuthorApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthorApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var authors = await _authorRepo.GetAllAsync(page, pageSize);
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name
            });
        }

        public async Task<AuthorDto?> GetByIdAsync(Guid id)
        {
            var a = await _authorRepo.GetByIdAsync(id);
            if (a == null) return null;
            return new AuthorDto
            {
                Id = a.Id,
                Name = a.Name
            };
        }

        public async Task<AuthorDto> AddAsync(AuthorDto authorDto)
        {
            // Normalizar nombre
            authorDto.Name = BookAuthorApi.Helpers.StringNormalizer.Normalize(authorDto.Name);
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = authorDto.Name
            };
            await _authorRepo.AddAsync(author);
            authorDto.Id = author.Id;
            return authorDto;
        }

        public async Task<AuthorDto?> UpdateAsync(Guid id, AuthorDto authorDto)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null) return null;
            // Normalizar nombre
            authorDto.Name = BookAuthorApi.Helpers.StringNormalizer.Normalize(authorDto.Name);
            author.Name = authorDto.Name;
            await _authorRepo.UpdateAsync(author);
            return authorDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _authorRepo.DeleteAsync(id);
        }
    }
}
