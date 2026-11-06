using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.API.Common
{
    //[Authorize(Policy = "AuthZPolicy")]
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string? EntraUserId =>
        User.FindFirst("oid")?.Value ??
        User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        protected ActionResult ProblemResponse(
        string title,
        string detail,
        int status)
        {
            return StatusCode(status, new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Status = status
            });
        }
        
    }
}
