using BookAuthorApi.Controllers;
using BookAuthorApi.DTOs;
using BookAuthorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookAuthorApi.Tests
{
    public class BooksControllerTests
    {
        [Fact]
        public async Task Create_ShouldReturnCreated_WhenValid()
        {
            var serviceMock = new Mock<IBookService>();
            var dto = new BookDto { Id = Guid.NewGuid(), Isbn = "1234567890123", Title = "Titulo", PublicationYear = 2020, AuthorId = Guid.NewGuid() };
            serviceMock.Setup(s => s.AddAsync(It.IsAny<BookDto>())).ReturnsAsync(dto);
            var controller = new BooksController(serviceMock.Object);
            var result = await controller.Create(dto);
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<BookDto>(created.Value);
            Assert.Equal(dto.Id, returned.Id);
        }
    }
}
