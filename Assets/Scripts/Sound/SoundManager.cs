using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioSource touchAudioSource;
        [SerializeField] private AudioClip playerSound;
        [SerializeField] private AudioClip touchSound;
        
        public bool enabled;

        public void EnableSound()
        {
            enabled = true;
        }

        public void DisableSound()
        {
            enabled = false;
        }

        public void PlayFireSound()
        {
            if (!enabled) return;
            playerAudioSource.Play();
        }

        public void PlayTouchSound()
        {
            touchAudioSource.Play();
        }

        public void StopFireSound()
        {
            playerAudioSource.Stop();
        }
    }
}