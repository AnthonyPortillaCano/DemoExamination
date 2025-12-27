using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAuthorApi.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
