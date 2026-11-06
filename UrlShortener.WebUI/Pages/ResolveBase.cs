using Microsoft.AspNetCore.Components;
using UrlShortener.WebUI.Services;
using Microsoft.JSInterop;
namespace UrlShortener.WebUI.Pages
{
    public class ResolveBase:ComponentBase
    {
        [Parameter]
        public string code { get; set; } = string.Empty;

        [Inject]
        protected IShortLinkService ShortLinkService { get; set; } = default!;

        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;
        [Inject]
        protected IJSRuntime JS { get; set; } = default!;
        protected bool Loading { get; set; } = true;

        protected bool NotFound { get; set; }

        protected bool RequiresPassword { get; set; }

        protected string Password { get; set; } = string.Empty;

        protected string? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // We will implement the API resolution here next.
            var result =await ShortLinkService.GetResolveInfoAsync(code);

            if (result == null)
            {
                NotFound = true;
                Loading = false;
                return;
            }

            if (result.IsProtected)
            {
                RequiresPassword = true;
                Loading = false;
                return;
            }
            // Public link:
            //ResolveAsync validates the link and records the visit.
            var originalUrl =
                await ShortLinkService.ResolveAsync(code, null);

            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                NotFound = true;
                Loading = false;
                return;
            }

            // Leave the resolver page and go to the destination.
            await RedirectToOriginalUrl(originalUrl);
            //await JS.InvokeVoidAsync(
            //    "location.replace",
            //    originalUrl);
        }

        protected async Task SubmitPassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter the password.";
                return;
            }

            ErrorMessage = null;
            Loading = true;

            var originalUrl =
                await ShortLinkService.ResolveAsync(
                    code,
                    Password);

            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                ErrorMessage =
                    "Incorrect password or the link is no longer available.";

                Loading = false;
                return;
            }
            await RedirectToOriginalUrl(originalUrl);
            //Navigation.NavigateTo(originalUrl, forceLoad: true);

            //await JS.InvokeVoidAsync(
            //    "location.replace",
            //    originalUrl);
        }
        private async Task RedirectToOriginalUrl(string originalUrl)
        {
            Console.WriteLine($"Redirecting to: {originalUrl}");

            await JS.InvokeVoidAsync(
                "location.replace",
                originalUrl);
        }
    }
}
