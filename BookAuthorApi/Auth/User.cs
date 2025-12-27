using System;
using System.ComponentModel.DataAnnotations;

namespace BookAuthorApi.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
