using UnityEngine;

namespace Sound.UI
{
    public class FinishLevelSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource starAudioSource;
        [SerializeField] private AudioSource pointCounterSound;
        [SerializeField] private AudioSource levelBonusSound;

        public void ResetPitch()
        {
            starAudioSource.pitch = 1;
            levelBonusSound.pitch = 1;
        }
        
        public void PlayStarSound()
        {
            starAudioSource.Play();
            starAudioSource.pitch += 0.1f;
        }

        public void PlayPointCounter()
        {
            pointCounterSound.Play();
        }

        public void PlayLevelBonusSound()
        {
            levelBonusSound.Play();
            levelBonusSound.pitch += 0.1f;
        }

        public void StopPointCounter()
        {
            pointCounterSound.Stop();
        }
    }
}