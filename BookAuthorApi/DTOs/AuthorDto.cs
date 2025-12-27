using System;
using System.ComponentModel.DataAnnotations;

namespace BookAuthorApi.DTOs
{
    public class AuthorDto
    {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
