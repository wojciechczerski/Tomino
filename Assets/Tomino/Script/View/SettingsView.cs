using Tomino;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    public Text titleText;
    public Toggle musicToggle;
    public Toggle screenButtonsToggle;
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
        screenButtonsToggle.onValueChanged.AddListener(screenButonsEnabled =>
        {
            Settings.ScreenButonsEnabled = screenButonsEnabled;
            PlayToggleAudioClip(screenButonsEnabled);
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
