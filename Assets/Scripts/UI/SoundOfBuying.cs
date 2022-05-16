using System;
using UnityEngine;

namespace Core
{
    public class SoundOfBuying : MonoBehaviour
    {
        private AudioSource audioSource;
        [SerializeField] private AudioClip buyingSound;
        [SerializeField] private AudioClip failBuyingSound;
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayBuyingSound()
        {
            audioSource.clip = buyingSound;
            audioSource.Play();
        }
        
        public void PlayFailBuyingSound()
        {
            audioSource.clip = buyingSound;
            audioSource.Play();
        }
    }
}