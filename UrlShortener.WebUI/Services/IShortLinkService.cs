using UrlShortener.Shared.Models;

namespace UrlShortener.WebUI.Services
{
    public interface IShortLinkService
    {
        Task<List<ShortLinkListItemDto>?> GetLinksAsync();
        Task<ShortLinkResponse?> CreateAsync(CreateShortLinkRequest request);
        Task<string?> ResolveAsync(string code);
        Task<bool> DeleteAsync(Guid id);
        Task<ShortLinkStatsDto?> GetStatsAsync(Guid id);
        Task<string> ResolveAsync(string code, string? password);
        Task<ResolveInfoDto?> GetResolveInfoAsync(string code);

    }
}
