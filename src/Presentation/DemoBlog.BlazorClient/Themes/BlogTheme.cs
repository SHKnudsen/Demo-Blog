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
                AppbarBackground = new MudColor("#F2DFD1"),
            };

            this.PaletteDark = new Palette
            {
                AppbarBackground = new MudColor("#51453A")
            };
        }
    }
}
