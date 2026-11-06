using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models
{
    public class ResolveInfoDto
    {
        public string OriginalUrl { get; set; } = string.Empty;

        public bool IsProtected { get; set; }
    }
}
