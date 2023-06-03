using MudBlazor;
using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Themes
{
    public class ThemeSurfacePalette
    {
        private readonly MudTheme _theme;

        public MudColor Surface1 => _theme.Palette.Primary.SetAlpha(0.05);
        public MudColor Surface2 => _theme.Palette.Primary.SetAlpha(0.08);
        public MudColor Surface3 => _theme.Palette.Primary.SetAlpha(0.11);
        public MudColor Surface4 => _theme.Palette.Primary.SetAlpha(0.12);
        public MudColor Surface5 => _theme.Palette.Primary.SetAlpha(0.14);

        public ThemeSurfacePalette(MudTheme theme)
        {
            _theme = theme;
        }
    }
}
