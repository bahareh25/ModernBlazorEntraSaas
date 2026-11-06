using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models
{
    public class ResolveResult
    {
        public Guid Id { get; set; }

        public string OriginalUrl { get; set; } = string.Empty;
    }
}
