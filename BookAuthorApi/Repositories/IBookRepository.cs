using BookAuthorApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAuthorApi.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(string? title = null, string? authorName = null, int page = 1, int pageSize = 10);
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<bool> DeleteAsync(Guid id);
    }
}
