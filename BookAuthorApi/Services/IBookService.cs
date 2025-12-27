using BookAuthorApi.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAuthorApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(string? title = null, string? authorName = null, int page = 1, int pageSize = 10);
        Task<BookDto?> GetByIdAsync(Guid id);
        Task<BookDto> AddAsync(BookDto bookDto);
        Task<BookDto?> UpdateAsync(Guid id, BookDto bookDto);
        Task<bool> DeleteAsync(Guid id);
        Task<BookCsvUploadResultDto> UploadCsvAsync(System.IO.Stream csvStream);
    }
}
