using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

    public class ShortLink:BaseEntity
    {

        public string OriginalUrl { get;  set; }

        public string ShortCode { get;  set; }

        public DateTime CreatedAt { get;  set; }

        public DateTime? ExpirationDate { get;  set; }

        public string? PasswordHash { get;  set; }

        public Guid UserId { get;  set; }

        public User User { get;  set; }

        public ICollection<Visit> Visits { get; set; }
            = new List<Visit>();
    public ShortLink()
    {
    }

    public ShortLink(
        string originalUrl,
        string shortCode,
        Guid userId,
        DateTime? expirationDate = null,
        string? passwordHash = null)
    {
        Id = Guid.NewGuid();

        OriginalUrl = originalUrl;

        ShortCode = shortCode;

        UserId = userId;

        ExpirationDate = expirationDate;

        PasswordHash = passwordHash;

        CreatedAt = DateTime.UtcNow;
    }



}

