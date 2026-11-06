using System.Net.Http;
using System.Net.Http.Json;
using UrlShortener.Shared.Models;
using static System.Net.WebRequestMethods;

namespace UrlShortener.WebUI.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _publicApiClient;

        public ShortLinkService(HttpClient httpClient, HttpClient publicApiClient)
        {
            this._httpClient = httpClient;
            this._publicApiClient = publicApiClient;
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

        public async Task<ResolveInfoDto?> GetResolveInfoAsync(string code)
        {
            var response = await _publicApiClient.GetAsync(
        $"api/links/resolve/{Uri.EscapeDataString(code)}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<ResolveInfoDto>();
        }

        public async Task<ShortLinkStatsDto?> GetStatsAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ShortLinkStatsDto>($"api/links/{id}/stats");
        }

        public async Task<string?> ResolveAsync(string code)
        {
            return await _httpClient.GetFromJsonAsync<string>($"api/links/{code}");
        }

        public async Task<string> ResolveAsync(string code, string? password)
        {
            var request = new ResolveRequest
            {
                Password = password
            };

            var response = await _publicApiClient.PostAsJsonAsync(
                $"api/links/resolve/{Uri.EscapeDataString(code)}",
                request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                Console.WriteLine(
                    $"Resolve failed: {response.StatusCode}");

                Console.WriteLine(error);

                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
