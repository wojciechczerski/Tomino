using UnityEngine;

namespace Tomino
{
    public static class Settings
    {
        public delegate void SettingsDelegate();
        public static SettingsDelegate ChangedEvent = delegate { };

        private static readonly string musicEnabledKey = "tomino.settings.musicEnabled";
        private static readonly string screenButtonsEnabledKey = "tomino.settings.screenButtonsEnabled";

        public static bool MusicEnabled
        {
            get
            {
                return PlayerPrefs.GetInt(musicEnabledKey, 1).BoolValue();
            }

            set
            {
                PlayerPrefs.SetInt(musicEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                ChangedEvent.Invoke();
            }
        }

        public static bool ScreenButonsEnabled
        {
            get
            {
                return PlayerPrefs.GetInt(screenButtonsEnabledKey, 0).BoolValue();
            }

            set
            {
                PlayerPrefs.SetInt(screenButtonsEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                ChangedEvent.Invoke();
            }
        }
    }
}
