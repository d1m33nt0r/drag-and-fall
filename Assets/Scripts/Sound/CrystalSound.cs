using UnityEngine;

namespace Sound
{
    public class CrystalSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Play()
        {
            audioSource.Play();
        }
    }
}