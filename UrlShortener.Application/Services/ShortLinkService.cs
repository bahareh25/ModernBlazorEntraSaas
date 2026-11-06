using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UrlShortener.Application.Common.Enums;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Shared.Models;


namespace UrlShortener.Application.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly IShortLinkRepository _repo;
        private readonly IUserService _users;
        private readonly IVisitRepository _visits;
        private readonly IPasswordHasherService _passwordHasher;

        public ShortLinkService(IShortLinkRepository repo, IUserService users,IVisitRepository visits, IPasswordHasherService passwordHasher)
        {
           _repo = repo;
           _users = users;
           _visits = visits;
           _passwordHasher = passwordHasher;
        }

        public async Task<ShortLinkResponse> CreateAsync(CreateShortLinkRequest request, string entraUserId)
         
        {
            var user = await _users.GetOrCreateAsync(entraUserId);

            if (user == null)
                return null;
            string? passwordHash = null;
            var code = await GenerateUniqueCodeAsync();
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                passwordHash = _passwordHasher.Hash(request.Password);
            }

            var link = new ShortLink(request.Url, code, user.Id,request.ExpirationDate, passwordHash);

            await _repo.AddAsync(link);
        

            return new ShortLinkResponse
            {
                ShortCode = code,
                OriginalUrl = request.Url,
                ShortUrl = $"https://localhost:5001/{code}",
              
            };
        }


        public async Task<string?> ResolveAsync(string code)
        {
            var link = await _repo.GetByCodeAsync(code);

            if (link == null)
                return null;

            return link.OriginalUrl;
        }
        private async Task<string> GenerateUniqueCodeAsync()
        {
            string code;

            do
            {
                code = GenerateCode();
            }
            while (await _repo.ExistsAsync(code));

            return code;
        }

        private string GenerateCode()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Range(0, 6)
                .Select(_ => chars[Random.Shared.Next(chars.Length)])
                .ToArray());
        }

        public async Task<List<ShortLinkListItemDto>> GetUserLinksAsync(string entraUserId)
        {
            // 1. Find user by Entra ID
            var user = await _users.GetOrCreateAsync(entraUserId);

            if (user == null)
                return null; // throw new Exception("User not found");

           // 2.Get links from repository
           var links = await _repo.GetByUserIdAsync(user.Id);//user.Id
            if (links == null)
                return null;

            // 3. Map to DTO
            return links.Select(x => new ShortLinkListItemDto
            {
                Id = x.Id,
                ShortCode = x.ShortCode,
                OriginalUrl = x.OriginalUrl,
                CreatedAt = x.CreatedAt,
                ExpirationDate = x.ExpirationDate,
                PasswordHash = x.PasswordHash,
                ShortUrl = $"https://localhost:5001/{x.ShortCode}"
            }).ToList();
        }

        public async Task<DeleteLinkStatus> DeleteAsync(Guid id, string entraUserId)
        {
            // 1. Find authenticated user
            var user = await _users.GetOrCreateAsync(entraUserId);

            if (user == null)
                return DeleteLinkStatus.UserNotFound;//throw new Exception("User not found");

            //2.Find link
           var link = await _repo.GetByIdAsync(id);

            if (link == null)
                return DeleteLinkStatus.LinkNotFound; // throw new Exception("Link not found");

            //// 3. Ownership validation
            //if (link.UserId != user.Id)
            //    throw new UnauthorizedAccessException("You do not own this link");

            // 4. Delete
            _repo.Remove(link);

            // 5. Save
            await _repo.SaveAsync();
            return DeleteLinkStatus.Success;
        }

        public async Task<ShortLinkStatsDto> GetStatsAsync(Guid id, string entraUserId)
        {
            var user = await _users.GetOrCreateAsync(entraUserId);

            if (user == null)
                //throw new Exception("User not found");
                return null;

            var link = await _repo.GetByIdAsync(id);

            if (link == null || link.UserId != user.Id)
                //throw new Exception("Link not found");
                return null;

            //if (link.UserId != user.Id)
            //    throw new UnauthorizedAccessException();

            var count = await _visits.CountByShortLinkIdAsync(id);
            var lastClick = await _visits.GetLastClickAsync(id);

            return new ShortLinkStatsDto
            {
                ShortLinkId = id,
                VisitCount = count,
                LastClickAt = lastClick
            };
        }

        public async Task<ResolveResult?> ResolveAsync(string code, string? password)
        {
            var link = await _repo.GetByCodeAsync(code);

            if (link == null)
                return null;// ResolveResult.Fail("Link not found");

            // Optional: expiration check
            if (link.ExpirationDate.HasValue &&
                link.ExpirationDate.Value < DateTime.UtcNow)
                return null;// ResolveResult.Fail("Link expired");
                            // Public link
            if (string.IsNullOrEmpty(link.PasswordHash))
            {
                return new ResolveResult
                {
                    Id = link.Id,
                    OriginalUrl = link.OriginalUrl
                };
            }
            // Protected link
            if (string.IsNullOrWhiteSpace(password))
                return null;
            
            // Password protection check

                var isValid = _passwordHasher.Verify(link.PasswordHash, password);

                if (!isValid)
                    return null;/// ResolveResult.Fail("Invalid password");
            

            return new ResolveResult
            {
                Id = link.Id,
               // ShortCode = link.ShortCode,
                OriginalUrl = link.OriginalUrl,
               // CreatedAt = link.CreatedAt,
               // ExpirationDate = link.ExpirationDate,
               // PasswordHash = link.PasswordHash,
               // ShortUrl = $"https://localhost:5001/{link.ShortCode}"
            };
        }

        public async  Task<ResolveInfoDto?> GetResolveInfoAsync(string code)
        {
            var link = await _repo.GetByCodeAsync(code);

            if (link == null)
                return null;

            if (link.ExpirationDate.HasValue &&
                link.ExpirationDate.Value < DateTime.UtcNow)
            {
                return null;
            }

            var isProtected =
                !string.IsNullOrEmpty(link.PasswordHash);

            return new ResolveInfoDto
            {
                IsProtected = isProtected,

                OriginalUrl = isProtected
                    ? null
                    : link.OriginalUrl
            };
        }

      
    }
}
