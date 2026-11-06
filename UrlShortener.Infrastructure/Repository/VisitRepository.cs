using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Common;

namespace UrlShortener.Infrastructure.Repository
{
    public class VisitRepository : IVisitRepository
    {
        private readonly AppDbContext _appDbContext;

        public VisitRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<Visit> AddAsync(Visit visit)
        {


            var result = await _appDbContext.AddAsync(visit);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> CountByShortLinkIdAsync(Guid shortLinkId)
        {
            return await _appDbContext.Visits
                .CountAsync(v => v.ShortLinkId == shortLinkId);
        }

        public async Task<DateTime?> GetLastClickAsync(Guid shortLinkId)
        {
            return await _appDbContext.Visits
        .Where(v => v.ShortLinkId == shortLinkId)
        .OrderByDescending(v => v.VisitedAt)
        .Select(v => v.VisitedAt)
        .FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
