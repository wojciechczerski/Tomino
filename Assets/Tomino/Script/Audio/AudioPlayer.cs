using UnityEngine;

namespace Tomino.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioClip pauseClip;
        public AudioClip resumeClip;
        public AudioClip newGameClip;
        public AudioClip pieceMoveClip;
        public AudioClip pieceRotateClip;
        public AudioClip pieceDropClip;
        private AudioSource _audioSource;

        public void PlayPauseClip()
        {
            _audioSource.PlayOneShot(pauseClip);
        }

        public void PlayResumeClip()
        {
            _audioSource.PlayOneShot(resumeClip);
        }

        public void PlayNewGameClip()
        {
            _audioSource.PlayOneShot(newGameClip);
        }

        public void PlayPieceMoveClip()
        {
            _audioSource.PlayOneShot(pieceMoveClip);
        }

        public void PlayPieceRotateClip()
        {
            _audioSource.PlayOneShot(pieceRotateClip);
        }

        public void PlayPieceDropClip()
        {
            _audioSource.PlayOneShot(pieceDropClip);
        }

        public void PlayToggleOnClip()
        {
            _audioSource.PlayOneShot(resumeClip);
        }

        public void PlayToggleOffClip()
        {
            _audioSource.PlayOneShot(pauseClip);
        }

        internal void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
