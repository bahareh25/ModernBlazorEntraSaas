using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using UrlShortener.Shared.Models;
using UrlShortener.WebUI.Services;

namespace UrlShortener.WebUI.Pages
{
    public class ShortLinkListBase : ComponentBase
    {
        [Inject]
        public IShortLinkService ShortLinkService { get; set; }
        [Inject]
        protected IDialogService DialogService { get; set; } = default!;
        public List<ShortLinkListItemDto> links { get; set; }
        [Inject]
        protected IJSRuntime JS { get; set; } = default!;
        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;
     
        protected override async Task OnInitializedAsync()
        {
            links = await ShortLinkService.GetLinksAsync() ?? new List<ShortLinkListItemDto>();
        }
        public async Task OpenCreateDialog()
        {
            var dialog = DialogService.Show<CreateLinkDialog>(
                "Create Short Link");

            var result = await dialog.Result;

            if (!result.Cancelled && result.Data is ShortLinkResponse link)
            {
                links.Add(new ShortLinkListItemDto
                {
                     //Id = link.Id,
                    ShortUrl = link.ShortUrl,
                    OriginalUrl = link.OriginalUrl,
                    //CreatedAt = link.CreatedAt,
                    //ExpirationDate = link.ExpirationDate,
                    ShortCode = link.ShortCode,
                    //IsProtected = link.IsProtected
                });

                StateHasChanged();
            }
        }

        protected async Task ConfirmDelete(Guid id)
        {
            var parameters = new DialogParameters
    {
        { "ContentText", "Are you sure you want to delete this link?" },
        { "ButtonText", "Delete" },
        { "Color", Color.Error }
    };

            var dialog = DialogService.Show<ConfirmDialog>("Confirm Delete", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await Delete(id);
            }
        }
        protected async Task Delete(Guid id)
        {
            await ShortLinkService.DeleteAsync(id);

            await LoadLinks(); // refresh from DB
        }
        protected async Task LoadLinks()
        {
            links = await ShortLinkService.GetLinksAsync() ?? new List<ShortLinkListItemDto>();
            StateHasChanged();
        }

        protected async Task Copy(string text)
        {
            await JS.InvokeVoidAsync("navigator.clipboard.writeText", text);

            Snackbar.Add("Copied to clipboard", Severity.Success);
        }
        protected async Task OpenStats(Guid id)
        {
            var stats = await ShortLinkService.GetStatsAsync(id);

            var parameters = new DialogParameters
            {
                ["Stats"] = stats,
             
            };
            var options = new DialogOptions
            {
                MaxWidth = MaxWidth.Medium,   // Small | Medium | Large | ExtraLarge | False
                FullWidth = true
            };

            DialogService.Show<StatesDialog>("Statistics", parameters,options);
        }
        protected async Task OpenShortLink(string code)
        {
            // Navigation.NavigateTo($"/r/{code}", forceLoad: true);
            var url = $"{Navigation.BaseUri}r/{code}";

            await JS.InvokeVoidAsync("open", url, "_blank");
        }
    }
}
