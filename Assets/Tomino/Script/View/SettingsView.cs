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

        screenButtonsToggle.isOn = Settings.ScreenButonsEnabled;
        screenButtonsToggle.GetComponentInChildren<Text>().text = Constant.Text.ScreenButtons;
        screenButtonsToggle.onValueChanged.AddListener(screenButtonsEnabled =>
        {
            Settings.ScreenButonsEnabled = screenButtonsEnabled;
            PlayToggleAudioClip(screenButtonsEnabled);
        });

        themeText.text = Constant.Text.Theme;

        defaultThemeToggle.isOn = Settings.Theme == Settings.ThemeType.Default;
        defaultThemeToggle.GetComponentInChildren<Text>().text = Constant.Text.DefaultTheme;
        defaultThemeToggle.onValueChanged.AddListener(toggleEnabled =>
        {
            Settings.Theme = Settings.ThemeType.Default;
            if (toggleEnabled)
            {
                PlayToggleAudioClip(true);
            }
        });

        autumnThemeToggle.isOn = Settings.Theme == Settings.ThemeType.Autumn;
        autumnThemeToggle.GetComponentInChildren<Text>().text = Constant.Text.AutumnTheme;
        autumnThemeToggle.onValueChanged.AddListener(toggleEnabled =>
        {
            Settings.Theme = Settings.ThemeType.Autumn;
            if (toggleEnabled)
            {
                PlayToggleAudioClip(true);
            }
        });

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
}
