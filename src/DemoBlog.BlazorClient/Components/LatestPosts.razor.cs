using DemoBlog.BlazorClient.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Components
{
    public partial class LatestPosts
    {
        [Parameter]
        public MudColor BackgroundColor { get; set; }
    }
}
