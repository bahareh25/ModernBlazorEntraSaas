using Microsoft.AspNetCore.Components;
using MudBlazor;
using UrlShortener.Shared.Models;
using UrlShortener.WebUI.Services;

namespace UrlShortener.WebUI.Pages
{
    public class CreateLinkDialogBase:ComponentBase
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject]
        public IShortLinkService ShortLinkService { get; set; }

        public CreateShortLinkRequest model = new();

        public async Task Create()
        {
            var result = await ShortLinkService.CreateAsync(model);

            if (result != null)
            {
                MudDialog.Close(DialogResult.Ok(result));
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}

