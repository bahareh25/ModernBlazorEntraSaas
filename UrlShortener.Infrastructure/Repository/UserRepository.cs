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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task<User?> GetByEntraIdAsync(string entraObjectId)
        {
            return await _context.Users
        .FirstOrDefaultAsync(u => u.EntraObjectId == entraObjectId);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async  Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
