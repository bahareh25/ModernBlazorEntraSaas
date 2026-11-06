using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models
{
    public class ShortLinkStatsDto
    {
        public Guid ShortLinkId { get; set; }

        public int VisitCount { get; set; }

        public DateTime? LastClickAt { get; set; }
    }
}
