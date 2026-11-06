using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces;

public interface IVisitRepository
{
    Task<Visit> AddAsync(Visit visit);
    Task<int> CountByShortLinkIdAsync(Guid shortLinkId);
    Task<DateTime?> GetLastClickAsync(Guid shortLinkId);
    Task SaveAsync();
}
