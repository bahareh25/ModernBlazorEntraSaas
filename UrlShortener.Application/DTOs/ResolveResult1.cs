using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace UrlShortener.Application.DTOs
{
    public class ResolveResult1
    {
        public bool Success { get; set; }
        public string? Url { get; set; }
        public string? Error { get; set; }

        public static ResolveResult1 Ok(string url)
            => new() { Success = true, Url = url };

        public static ResolveResult1 Fail(string error)
            => new() { Success = false, Error = error };
    }

}
