using UnityEngine;

namespace Sound
{
    public class KeySound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Play()
        {
            audioSource.Play();
        }
    }
}