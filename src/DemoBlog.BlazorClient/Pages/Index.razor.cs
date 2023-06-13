using DemoBlog.BlazorClient.Services;
using Microsoft.AspNetCore.Components;

namespace DemoBlog.BlazorClient.Pages;

public partial class Index
{
    [Inject] 
    private LayoutService LayoutService { get; set; }

    public bool Starred { get; set; }

    protected override void OnInitialized()
    {
        LayoutService.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured;
        base.OnInitialized();
    }

    public void Dispose()
    {
        LayoutService.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured;
    }

    private void LayoutServiceOnMajorUpdateOccured(object? sender, EventArgs e)
    {
        StateHasChanged();
    }
}
