using BookAuthorApi.DTOs;
using Microsoft.AspNetCore.Http;
using BookAuthorApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookAuthorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] string? authorName, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var books = await _bookService.GetAllAsync(title, authorName, page, pageSize);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookDto bookDto)
        {
            var created = await _bookService.AddAsync(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookDto bookDto)
        {
            var updated = await _bookService.UpdateAsync(id, bookDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null)
                return BadRequest("No file uploaded");
            var result = await _bookService.UploadCsvAsync(file.OpenReadStream());
            return Ok(result);
        }
    }
}
