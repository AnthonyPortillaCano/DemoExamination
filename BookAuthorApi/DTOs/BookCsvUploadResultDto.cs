using System.Collections.Generic;

namespace BookAuthorApi.DTOs
{
    public class BookCsvUploadResultDto
    {
        public int Total { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
