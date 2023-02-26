using DemoBlog.BlazorClient.Themes;
using MudBlazor;

namespace DemoBlog.BlazorClient.Services
{
    public class LayoutService
    {
        private IUserPreferencesService _userPreferencesService;
        private UserPreferences _userPreferences;

        public bool IsDarkMode { get; private set; } = false;

        public BlogTheme CurrentTheme { get; private set; }

        public event EventHandler MajorUpdateOccured;
        
        public LayoutService(IUserPreferencesService userPreferencesService)
        {
            _userPreferencesService = userPreferencesService;
            CurrentTheme = new BlogTheme();
        }

        public void SetDarkMode(bool value)
        {
            IsDarkMode = value;
        }

        public async Task ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            _userPreferences.DarkTheme = IsDarkMode;
            await _userPreferencesService.SaveUserPreferences(_userPreferences);
            OnMajorUpdateOccured();
        }

        public async Task ApplyUserPreferences(bool isDarkModeDefaultTheme)
        {
            _userPreferences = await _userPreferencesService.LoadUserPreferences();
            if (_userPreferences != null)
            {
                IsDarkMode = _userPreferences.DarkTheme;
            }
            else
            {
                IsDarkMode = isDarkModeDefaultTheme;
                _userPreferences = new UserPreferences { DarkTheme = IsDarkMode };
                await _userPreferencesService.SaveUserPreferences(_userPreferences);
            }
        }
        
        private void OnMajorUpdateOccured() => MajorUpdateOccured?.Invoke(this, EventArgs.Empty);
    }
}
