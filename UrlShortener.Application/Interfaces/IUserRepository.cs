using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEntraIdAsync(string entraObjectId);
    Task<User?> GetByIdAsync(Guid id);

    Task<User> AddAsync(User user);

    Task SaveChangesAsync();
}
