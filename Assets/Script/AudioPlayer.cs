using UnityEngine;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip pieceMoveClip;
    public AudioClip pieceRotateClip;
    public AudioClip pieceDropClip;
    private AudioSource audioSource;

    public void PlayPieceMoveClip() => audioSource.PlayOneShot(pieceMoveClip);
    public void PlayPieceRotateClip() => audioSource.PlayOneShot(pieceRotateClip);
    public void PlayPieceDropClip() => audioSource.PlayOneShot(pieceDropClip);

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
