using UnityEngine;

namespace Sound
{
    public class PlatformSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void PlayDestroySound()
        {
            audioSource.Play();
            if(audioSource.pitch < 2) audioSource.pitch += .05f;
        }

        public void ResetPitch()
        {
            audioSource.pitch = 1;
        }
    }
}