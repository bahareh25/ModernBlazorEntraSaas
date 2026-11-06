using System.Net.Http;
using UrlShortener.Shared.Models;
using static System.Net.WebRequestMethods;

namespace UrlShortener.UI.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly HttpClient _httpClient;

        public ShortLinkService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<ShortLinkResponse?> CreateAsync(CreateShortLinkRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/links", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ShortLinkResponse>();
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
           var result= await _httpClient.DeleteAsync($"api/links/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<ShortLinkListItemDto>?> GetLinksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ShortLinkListItemDto>>("api/links");
        }

        public async Task<ShortLinkStatsDto?> GetStatsAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ShortLinkStatsDto>($"api/links/{id}/stats");
        }

        public Task<string?> ResolveAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<string> ResolveAsync(string code, string? password)
        {
            throw new NotImplementedException();
        }
    }
}
