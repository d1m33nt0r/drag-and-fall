using UnityEngine;

namespace Sound
{
    public class FailSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] failSounds;

        public void PlayFailSound()
        {
            audioSource.clip = failSounds[Random.Range(default, failSounds.Length)];
            audioSource.Play();
        }
    }
}