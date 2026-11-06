using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models
{
    public class ShortLinkResponse
    {
        public string ShortCode { get; set; } = string.Empty;

        public string OriginalUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;

    }
}
