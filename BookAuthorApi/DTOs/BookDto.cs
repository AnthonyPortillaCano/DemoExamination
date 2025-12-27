using System;
using System.ComponentModel.DataAnnotations;

namespace BookAuthorApi.DTOs
{
    public class BookDto
    {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string Isbn { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int PublicationYear { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
