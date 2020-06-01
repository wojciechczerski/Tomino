using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingsView : MonoBehaviour
{
    public Text titleText;
    public Toggle musicToggle;
    public Toggle screenButtonsToggle;
    public Button closeButton;
    public AudioPlayer audioPlayer;

    private UnityAction onCloseCallback;

    private void Awake()
    {
        titleText.text = Constant.Text.Settings;

        musicToggle.GetComponentInChildren<Text>().text = Constant.Text.Music;
        musicToggle.onValueChanged.AddListener((enabled) =>
        {
            PlayToggleAudioClip(enabled);
        });

        screenButtonsToggle.GetComponentInChildren<Text>().text = Constant.Text.ScreenButtons;
        screenButtonsToggle.onValueChanged.AddListener((enabled) =>
        {
            PlayToggleAudioClip(enabled);
        });

        closeButton.GetComponentInChildren<Text>().text = Constant.Text.Close;
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseCallback.Invoke();
        });

        closeButton.gameObject.GetComponent<PointerHandler>().onPointerDown.AddListener(() =>
        {
            audioPlayer.PlayResumeClip();
        });

        Hide();
    }

    public void Show(UnityAction onCloseCallback)
    {
        this.onCloseCallback = onCloseCallback;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void PlayToggleAudioClip(bool enabled)
    {
        if (enabled)
        {
            audioPlayer.PlayToggleOnClip();
        }
        else
        {
            audioPlayer.PlayToggleOffClip();
        }
    }
}
