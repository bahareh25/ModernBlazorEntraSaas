using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(Guid id);

        Task<User?> GetByEntraIdAsync(string entraObjectId);

        Task<User> CreateAsync(string entraObjectId);

        Task<User> GetOrCreateAsync(string entraObjectId);
    }
}
