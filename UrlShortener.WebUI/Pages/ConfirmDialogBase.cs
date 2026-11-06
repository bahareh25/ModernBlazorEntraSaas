using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace UrlShortener.WebUI.Pages
{
    public class ConfirmDialogBase: ComponentBase
    {
        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Parameter] public string ContentText { get; set; }
        [Parameter] public string ButtonText { get; set; }
        [Parameter] public Color Color { get; set; }

        public void Cancel() => MudDialog.Cancel();
        public void Confirm() => MudDialog.Close(DialogResult.Ok(true));
    }
}

