using UnityEngine;

namespace Sound.UI
{
    public class UISwooshSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void SwooshEffect()
        {
            audioSource.Play();
        }
    }
}