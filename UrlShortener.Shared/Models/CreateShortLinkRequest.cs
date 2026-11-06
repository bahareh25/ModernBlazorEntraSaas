using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.Models;

public class CreateShortLinkRequest
{
    [Required(ErrorMessage = "URL must be provided")]
    [StringLength(100, MinimumLength = 2)]
    public string Url { get; set; } = string.Empty;

    public DateTime? ExpirationDate { get; set; }

    public string? Password { get; set; }
}
