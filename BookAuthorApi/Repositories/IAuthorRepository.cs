using BookAuthorApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAuthorApi.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Author?> GetByIdAsync(Guid id);
        Task<Author> AddAsync(Author author);
        Task<Author?> UpdateAsync(Author author);
        Task<bool> DeleteAsync(Guid id);
        Task<Author?> GetByNameAsync(string name);
    }
}
