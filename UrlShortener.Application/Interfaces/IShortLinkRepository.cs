using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces;

public interface IShortLinkRepository
{
    Task<ShortLink?> GetByCodeAsync(string code);

    Task<ShortLink> AddAsync(ShortLink link);
    Task<bool> ExistsAsync(string code);
    Task<List<ShortLink>> GetByUserIdAsync(Guid userId);
    Task<ShortLink?> GetByIdAsync(Guid id);

    void Remove(ShortLink link);
    Task SaveAsync();

}
