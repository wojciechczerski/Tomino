using Tomino.Audio;
using Tomino.Model;
using Tomino.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace Tomino.View
{
    public class SettingsView : MonoBehaviour
    {
        public Text titleText;
        public Toggle musicToggle;
        public Toggle screenButtonsToggle;
        public Text themeText;
        public Toggle defaultThemeToggle;
        public Toggle autumnThemeToggle;
        public Toggle summerThemeToggle;
        public Toggle tealThemeToggle;
        public Button closeButton;
        public AudioPlayer audioPlayer;

        private UnityAction _onCloseCallback;

        internal void Awake()
        {
            musicToggle.isOn = Settings.MusicEnabled;
            musicToggle.onValueChanged.AddListener(musicEnabled =>
            {
                Settings.MusicEnabled = musicEnabled;
                PlayToggleAudioClip(musicEnabled);
            });

            screenButtonsToggle.isOn = Settings.ScreenButtonsEnabled;
            screenButtonsToggle.onValueChanged.AddListener(screenButtonsEnabled =>
            {
                Settings.ScreenButtonsEnabled = screenButtonsEnabled;
                PlayToggleAudioClip(screenButtonsEnabled);
            });

            ConfigureThemeToggle(defaultThemeToggle, Settings.ThemeType.Default);
            ConfigureThemeToggle(autumnThemeToggle, Settings.ThemeType.Autumn);
            ConfigureThemeToggle(summerThemeToggle, Settings.ThemeType.Summer);
            ConfigureThemeToggle(tealThemeToggle, Settings.ThemeType.Teal);

            closeButton.onClick.AddListener(() =>
            {
                Hide();
                _onCloseCallback.Invoke();
            });

            closeButton.gameObject.GetComponent<PointerHandler>().onPointerDown.AddListener(() =>
            {
                audioPlayer.PlayResumeClip();
            });

            Hide();
        }

        public void Show(UnityAction onCloseCallback)
        {
            _onCloseCallback = onCloseCallback;
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void PlayToggleAudioClip(bool audioEnabled)
        {
            if (audioEnabled)
            {
                audioPlayer.PlayToggleOnClip();
            }
            else
            {
                audioPlayer.PlayToggleOffClip();
            }
        }

        private void ConfigureThemeToggle(Toggle themeToggle, Settings.ThemeType themeType)
        {
            themeToggle.isOn = Settings.Theme == themeType;
            themeToggle.onValueChanged.AddListener(toggleEnabled =>
            {
                Settings.Theme = themeType;
                if (toggleEnabled)
                {
                    PlayToggleAudioClip(true);
                }
            });
        }
    }
}
