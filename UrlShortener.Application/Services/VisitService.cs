using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Services
{
    public class VisitService:IVisitService
    {
        private readonly IVisitRepository _repo;
       // private readonly AppDbContext _context;

        public VisitService(IVisitRepository repo)
        {
            _repo = repo;
           /// _context = context;
        }

        // 🔥 Track a visit (core logic)
        public async Task TrackAsync(Guid shortLinkId, string? ipAddress = null, string? userAgent = null)
        {
            var visit = new Visit
            {
                ShortLinkId = shortLinkId,
                VisitedAt = DateTime.UtcNow,
                IpAddress = ipAddress,
                UserAgent = userAgent
            };

            await _repo.AddAsync(visit);
            await _repo.SaveAsync();
        }

        // 📊 Visit count
        public async Task<int> GetVisitCountAsync(Guid shortLinkId)
        {
            return await _repo.CountByShortLinkIdAsync(shortLinkId);
        }

        // ⏱ Last visit
        public async Task<DateTime?> GetLastVisitAsync(Guid shortLinkId)
        {
            return await _repo.GetLastClickAsync(shortLinkId);
        }
    }
}
