using MudBlazor;
using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Themes
{
    public class BlogTheme : MudTheme
    {
        public BlogTheme()
        {
            this.Palette = new Palette
            {
                Primary = new MudColor("#8B5000"),
                Secondary = new MudColor("#785900"),
                Tertiary = new MudColor("#735C00"),
            };
        }
    }
}
