using BookAuthorApi.Data;
using BookAuthorApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthorApi.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Authors.Include(a => a.Books).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(Guid id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> UpdateAsync(Author author)
        {
            var existing = await _context.Authors.FindAsync(author.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(author);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Author?> GetByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
        }
    }
}
