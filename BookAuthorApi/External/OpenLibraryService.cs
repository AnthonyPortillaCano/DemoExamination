using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookAuthorApi.External
{
    public class OpenLibraryService
    {
        private readonly HttpClient _httpClient;
        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<string?> GetCoverUrlAsync(string isbn)
        {
            var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var key = $"ISBN:{isbn}";
            if (root.TryGetProperty(key, out var bookInfo))
            {
                if (bookInfo.TryGetProperty("cover", out var cover))
                {
                    if (cover.TryGetProperty("large", out var large))
                        return large.GetString();
                    if (cover.TryGetProperty("medium", out var medium))
                        return medium.GetString();
                    if (cover.TryGetProperty("small", out var small))
                        return small.GetString();
                }
            }
            return null;
        }
    }
}
