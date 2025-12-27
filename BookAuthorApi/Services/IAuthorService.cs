using BookAuthorApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAuthorApi.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<AuthorDto?> GetByIdAsync(Guid id);
        Task<AuthorDto> AddAsync(AuthorDto authorDto);
        Task<AuthorDto?> UpdateAsync(Guid id, AuthorDto authorDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
