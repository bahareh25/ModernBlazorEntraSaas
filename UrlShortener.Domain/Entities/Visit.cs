using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

public class Visit : BaseEntity
{

    public Guid ShortLinkId { get; set; }

    public ShortLink ShortLink { get; set; }

    public DateTime VisitedAt { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }
    //private Visit() { }

    //public Visit(Guid linkId, string? ip)
    //{
    //    Id = Guid.NewGuid();
    //    ShortLinkId = linkId;
    //    IpAddress = ip;
    //    VisitedAt = DateTime.UtcNow;
    //}
}


    
