using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models
{
    public class ShortLinkListItemDto
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }

        public string ShortCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? PasswordHash { get; set; }
        public string ShortUrl { get; set; }

    }
}
