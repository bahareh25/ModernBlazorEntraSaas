using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Common;

namespace UrlShortener.Infrastructure.Repository
{
    public class ShortLinkRepository : IShortLinkRepository
    {
        private readonly AppDbContext _context;

        public ShortLinkRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<ShortLink> AddAsync(ShortLink link)
        {
            var result = await _context.ShortLinks.AddAsync(link);
            await _context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<bool> ExistsAsync(string code)
        {
            return await _context.ShortLinks.AnyAsync(p => p.ShortCode == code);
        }

        public async Task<ShortLink?> GetByCodeAsync(string code)
        {
            return await _context.ShortLinks
            .FirstOrDefaultAsync(x => x.ShortCode == code);
        }

        //public async Task<IEnumerable<ShortLink>> GetByUserIdAsync(Guid userId)
        //{
        //    return await _context.ShortLinks
        //        .Where(x => x.UserId == userId)
        //        .OrderByDescending(x => x.CreatedAt).ToListAsync();
        //}
        public async Task<ShortLink?> GetByIdAsync(Guid id)
        {
            var result= await _context.ShortLinks
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public void Remove(ShortLink link)
        {
            _context.ShortLinks.Remove(link);
        
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<ShortLink>> GetByUserIdAsync(Guid userId)
        {
            return await _context.ShortLinks
         .Where(x => x.UserId == userId)
         .OrderByDescending(x => x.CreatedAt)
         .ToListAsync();
        }
    }
}
