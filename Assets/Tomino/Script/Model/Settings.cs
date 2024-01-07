using UnityEngine;

namespace Tomino
{
    public static class Settings
    {
        public delegate void SettingsDelegate();
        public static SettingsDelegate changedEvent = delegate { };

        private const string MusicEnabledKey = "tomino.settings.musicEnabled";
        private const string ScreenButtonsEnabledKey = "tomino.settings.screenButtonsEnabled";

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

        public static bool ScreenButonsEnabled
        {
            get => PlayerPrefs.GetInt(ScreenButtonsEnabledKey, 0).BoolValue();

            set
            {
                PlayerPrefs.SetInt(ScreenButtonsEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                changedEvent.Invoke();
            }
        }
    }
}
