using UnityEngine;

namespace Tomino.View
{
    public class ThemeController: MonoBehaviour
    {
        public ThemeProvider themeProvider;
        public Theme defaultTheme;
        public Theme autumnTheme;
        public Theme pinkTheme;
        public Theme tealTheme;

        private void Awake()
        {
            Settings.changedEvent += UpdateCurrentTheme;
            UpdateCurrentTheme();
        }

        private void UpdateCurrentTheme()
        {
            themeProvider.currentTheme = Settings.Theme switch
            {
                Settings.ThemeType.Default => defaultTheme,
                Settings.ThemeType.Autumn => autumnTheme,
                Settings.ThemeType.Pink => pinkTheme,
                Settings.ThemeType.Teal => tealTheme,
                _ => defaultTheme
            };
        }
    }
}
