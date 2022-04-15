using UnityEngine;

namespace Sound
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioSource touchAudioSource;

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