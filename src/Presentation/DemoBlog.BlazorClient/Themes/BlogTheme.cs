using MudBlazor;
using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Themes
{
    public class BlogTheme : MudTheme
    {
        public ThemeSurfacePalette SurfacePalette { get; private set; }
        
        public BlogTheme()
        {
            this.Palette = new Palette
            {
                Primary = new MudColor("#8B5000"),
                Secondary = new MudColor("#785900"),
                Tertiary = new MudColor("#735C00"),
                Surface = "#FFFBFF",
                AppbarBackground = new MudColor("#F2DFD1"),
            };

            this.PaletteDark = new PaletteDark
            {
                Primary = "#FFB870",
                //Secondary = "#FABD00",
                //Tertiary = "#EBC23E",
                //Surface = "#201B16",
                //AppbarBackground = new MudColor("#51453A")
            };

            SetSurfacePalette();
        }

        private void SetSurfacePalette()
        {
            SurfacePalette = new ThemeSurfacePalette(this);
        }
    }
}
