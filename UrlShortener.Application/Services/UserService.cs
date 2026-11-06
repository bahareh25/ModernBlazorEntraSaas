using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository userRepository)
        {
            this._repo = userRepository;
        }
        public async Task<User> CreateAsync(string entraObjectId)
        {
            var user = new User
            {
                EntraObjectId = entraObjectId
            };

            await _repo.AddAsync(user);

            await _repo.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByEntraIdAsync(string entraObjectId)
        {
            return await _repo.GetByEntraIdAsync(entraObjectId);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<User> GetOrCreateAsync(string entraObjectId)
        {
            var user = await _repo.GetByEntraIdAsync(entraObjectId);

            if (user != null)
                return user;

            return await CreateAsync(entraObjectId);
        }
    }
}
