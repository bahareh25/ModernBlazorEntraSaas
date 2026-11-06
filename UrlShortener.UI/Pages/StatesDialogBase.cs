using Microsoft.AspNetCore.Components;
using MudBlazor;
using UrlShortener.Shared.Models;
using UrlShortener.UI.Services;

namespace UrlShortener.UI.Pages
{
    

    public class StatesDialogBase:ComponentBase
    {
        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
        
        [Parameter]
        public ShortLinkStatsDto Stats { get; set; }

        public void Close() => MudDialog.Cancel();
    }
}
