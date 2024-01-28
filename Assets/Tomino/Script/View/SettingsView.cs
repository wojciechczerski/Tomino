using Tomino;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        titleText.text = Constant.Text.Settings;

        musicToggle.isOn = Settings.MusicEnabled;
        musicToggle.GetComponentInChildren<Text>().text = Constant.Text.Music;
        musicToggle.onValueChanged.AddListener(musicEnabled =>
        {
            Settings.MusicEnabled = musicEnabled;
            PlayToggleAudioClip(musicEnabled);
        });

        screenButtonsToggle.isOn = Settings.ScreenButtonsEnabled;
        screenButtonsToggle.GetComponentInChildren<Text>().text = Constant.Text.ScreenButtons;
        screenButtonsToggle.onValueChanged.AddListener(screenButtonsEnabled =>
        {
            Settings.ScreenButtonsEnabled = screenButtonsEnabled;
            PlayToggleAudioClip(screenButtonsEnabled);
        });

        themeText.text = Constant.Text.Theme;

        ConfigureThemeToggle(defaultThemeToggle, Settings.ThemeType.Default, Constant.Text.DefaultTheme);
        ConfigureThemeToggle(autumnThemeToggle, Settings.ThemeType.Autumn, Constant.Text.AutumnTheme);
        ConfigureThemeToggle(summerThemeToggle, Settings.ThemeType.Summer, Constant.Text.SummerTheme);
        ConfigureThemeToggle(tealThemeToggle, Settings.ThemeType.Teal, Constant.Text.TealTheme);

        closeButton.GetComponentInChildren<Text>().text = Constant.Text.Close;
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

    private void ConfigureThemeToggle(Toggle themeToggle, Settings.ThemeType themeType, string themeName)
    {
        themeToggle.isOn = Settings.Theme == themeType;
        themeToggle.GetComponentInChildren<Text>().text = themeName;
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
