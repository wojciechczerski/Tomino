using System;
using Tomino.Shared;
using UnityEngine;

namespace Tomino.Model
{
    public static class Settings
    {
        public enum ThemeType
        {
            Default, Autumn, Summer, Teal
        }

        public delegate void SettingsDelegate();
        public static SettingsDelegate changedEvent = delegate { };

        private const string MusicEnabledKey = "tomino.settings.musicEnabled";
        private const string ScreenButtonsEnabledKey = "tomino.settings.screenButtonsEnabled";
        private const string ThemeTypeKey = "tomino.settings.themeType";

        public static bool MusicEnabled
        {
            get => PlayerPrefs.GetInt(MusicEnabledKey, 1).BoolValue();

            set
            {
                PlayerPrefs.SetInt(MusicEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                changedEvent.Invoke();
            }
        }

        public static bool ScreenButtonsEnabled
        {
            get => PlayerPrefs.GetInt(ScreenButtonsEnabledKey, 0).BoolValue();

            set
            {
                PlayerPrefs.SetInt(ScreenButtonsEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                changedEvent.Invoke();
            }
        }

        public static ThemeType Theme
        {
            get
            {
                var storedValue = PlayerPrefs.GetString(ThemeTypeKey, ThemeType.Default.ToString());
                return Enum.TryParse<ThemeType>(storedValue, out var themeType) ? themeType : ThemeType.Default;
            }

            set
            {
                PlayerPrefs.SetString(ThemeTypeKey, value.ToString());
                PlayerPrefs.Save();
                changedEvent.Invoke();
            }
        }
    }
}
