using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip pauseClip;
    public AudioClip resumeClip;
    public AudioClip newGameClip;
    public AudioClip pieceMoveClip;
    public AudioClip pieceRotateClip;
    public AudioClip pieceDropClip;
    private AudioSource audioSource;

    public void PlayPauseClip() => audioSource.PlayOneShot(pauseClip);
    public void PlayResumeClip() => audioSource.PlayOneShot(resumeClip);
    public void PlayNewGameClip() => audioSource.PlayOneShot(newGameClip);
    public void PlayPieceMoveClip() => audioSource.PlayOneShot(pieceMoveClip);
    public void PlayPieceRotateClip() => audioSource.PlayOneShot(pieceRotateClip);
    public void PlayPieceDropClip() => audioSource.PlayOneShot(pieceDropClip);
    public void PlayToggleOnClip() => audioSource.PlayOneShot(resumeClip);
    public void PlayToggleOffClip() => audioSource.PlayOneShot(pauseClip);

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
