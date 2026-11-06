using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.API.Common;
using UrlShortener.Application.Common.Enums;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Shared.Models;

namespace UrlShortener.API.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
   
    public class LinksController : BaseController
    {
        private readonly IShortLinkService _service;
        private readonly IVisitService _visitService;

        public LinksController(IShortLinkService service,IVisitService visitService)
        {
            _service = service;
            _visitService = visitService;
        }

        [HttpPost]
        public async Task<ActionResult<ShortLinkResponse>> Create(CreateShortLinkRequest request)
        {
            // In real app: from JWT (Entra ID)
            var entraUserId =EntraUserId;

            if (entraUserId == null)
                return Unauthorized();

            var result = await _service.CreateAsync(request, entraUserId);
            return Ok(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Resolve(string code)
        {
            var entraUserId = EntraUserId;
            if (entraUserId == null)
                return Unauthorized();
            var url = await _service.ResolveAsync(code);

            if (url == null)
                return NotFound();

            ///return Redirect(url);
            return Ok(url);
        }

        [HttpGet]
        public async Task<ActionResult<ShortLinkListItemDto>> GetAll()
        {
            // In real app: from JWT (Entra ID)
            var entraUserId = EntraUserId;
            if (entraUserId == null)
                return Unauthorized();

            var result = await _service.GetUserLinksAsync(entraUserId);//entraUserId"b3c1f7d2-9a11-4c88-9c21-8a3b0c9d1e55"
            if (result == null)
            {
                return ProblemResponse(
                    "Link not found",
                    "The requested short link does not exist or you do not have access.",
                    StatusCodes.Status404NotFound);

            }

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entraUserId = EntraUserId;

            if (entraUserId == null)
                return Unauthorized();

            var result= await _service.DeleteAsync(id, entraUserId); //id,entrauserid
            switch (result)
            {
                case DeleteLinkStatus.UserNotFound:
                    return ProblemResponse(
                        "User not found",
                        "The authenticated user does not exist.",
                        StatusCodes.Status404NotFound);

                case DeleteLinkStatus.LinkNotFound:
                    return ProblemResponse(
                        "Link not found",
                        "The requested link does not exist.",
                        StatusCodes.Status404NotFound);

                case DeleteLinkStatus.Unauthorized:
                    return ProblemResponse(
                        "Forbidden",
                        "You do not own this link.",
                        StatusCodes.Status403Forbidden);

                case DeleteLinkStatus.Success:
                    return NoContent();

                default:
                    return ProblemResponse(
                        "Server error",
                        "Unexpected error occurred.",
                        StatusCodes.Status500InternalServerError);
            }

            //return NoContent();
        }

       
        [HttpGet("{id:guid}/stats")]
        public async Task<ActionResult<ShortLinkStatsDto>> GetStats(Guid id)
        {
            var entraUserId = EntraUserId;

            if (entraUserId == null)
                return Unauthorized();

            var result = await _service.GetStatsAsync(id, entraUserId);//id,entraUserId

            if (result == null)
            {
                return ProblemResponse(
                    "Link not found",
                    "The requested short link does not exist or you do not have access.",
                    StatusCodes.Status404NotFound);
             
            }

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("resolve/{code}")]
        public async Task<ActionResult<string>> Resolve(string code, ResolveRequest request)
        {
            //var entraUserId = EntraUserId;

            //if (entraUserId == null)
            //    return Unauthorized(); 

            var result = await _service.ResolveAsync(code, request.Password);
            if (result == null)
                return ProblemResponse(
                    "Link not found",
                    "The requested short link does not exist or you do not have access.or password is invalid",
                    StatusCodes.Status404NotFound);      //BadRequest(result);
            await _visitService.TrackAsync(result.Id, HttpContext.Connection.RemoteIpAddress?.ToString(),
                Request.Headers.UserAgent.ToString());
            


            return Ok(result.OriginalUrl);
        }

        [AllowAnonymous]
        [HttpGet("resolve/{code}")]
        public async Task<IActionResult> GetResolveInfo(string code)
        {
            var result = await _service.GetResolveInfoAsync(code);

            if (result == null)
            {
                return ProblemResponse(
                    "Link not found",
                    "The requested short link does not exist or has expired.",
                    StatusCodes.Status404NotFound);
            }

            return Ok(result);
        }
    }
}
