using BookAuthorApi.Data;
using BookAuthorApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthorApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(string? title = null, string? authorName = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();
            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));
            if (!string.IsNullOrWhiteSpace(authorName))
                query = query.Where(b => b.Author.Name.Contains(authorName));
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
