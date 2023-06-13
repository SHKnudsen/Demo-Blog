using DemoBlog.BlazorClient.Services;
using Microsoft.AspNetCore.Components;

namespace DemoBlog.BlazorClient.Components
{
    public partial class BlogPostCard
    {
        [Inject] private LayoutService LayoutService { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Author { get; set; }

        [Parameter]
        public string Subtitle { get; set; }

        [Parameter]
        public string Summary { get; set; }

        [Parameter]
        public object CoverImage { get; set; }

        public bool Starred { get; set; }

    }
}
