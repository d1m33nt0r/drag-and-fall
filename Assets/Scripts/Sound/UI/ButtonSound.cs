using UnityEngine;

namespace Sound.UI
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Play()
        {
            audioSource.Play();
        }
    }
}