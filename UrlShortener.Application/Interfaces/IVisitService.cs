using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Application.Interfaces
{
    public interface IVisitService
    {
        Task TrackAsync(Guid shortLinkId, string? ipAddress = null, string? userAgent = null);

        Task<int> GetVisitCountAsync(Guid shortLinkId);

        Task<DateTime?> GetLastVisitAsync(Guid shortLinkId);
    }
}
