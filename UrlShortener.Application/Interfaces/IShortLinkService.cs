using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Common.Enums;
using UrlShortener.Application.DTOs;
using UrlShortener.Shared.Models;


namespace UrlShortener.Application.Interfaces
{
    public interface IShortLinkService
    {
        Task<ShortLinkResponse> CreateAsync(CreateShortLinkRequest request,string entraUserId);
        Task<List<ShortLinkListItemDto>> GetUserLinksAsync(string entraUserId);
        Task<string?> ResolveAsync(string code);
        Task<DeleteLinkStatus> DeleteAsync(Guid id, string entraUserId);
       Task<ShortLinkStatsDto> GetStatsAsync(Guid id, string entraUserId);
       Task<ResolveResult?> ResolveAsync(string code, string? password);
       Task<ResolveInfoDto?> GetResolveInfoAsync(string code);
    }
}
