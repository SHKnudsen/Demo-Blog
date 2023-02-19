using MudBlazor.Utilities;

namespace DemoBlog.BlazorClient.Themes
{
    public static class Styles
    {
        public static class Surface
        {
            private readonly static string _surface2Light = "background: linear-gradient(0deg, rgba(139, 80, 0, 0.08), rgba(139, 80, 0, 0.08)), #FFFBFF";
            private static readonly string _surface2Dark = "background: linear-gradient(0deg, rgba(255, 184, 112, 0.08), rgba(255, 184, 112, 0.08)), #201B16";
            
            public static string Surface2(bool isDarkMode)
                => isDarkMode ? _surface2Dark : _surface2Light;

        }
    }
}
