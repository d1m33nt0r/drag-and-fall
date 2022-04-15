using UnityEngine;

namespace Sound
{
    public class BonusSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bonusSoundManager;
        [SerializeField] private AudioClip multiplierSound;
        [SerializeField] private AudioClip magnetSound;
        [SerializeField] private AudioClip accelerationSound;
        [SerializeField] private AudioClip shieldSound;

        public void PlayMagnetSound()
        {
            bonusSoundManager.clip = magnetSound;
            bonusSoundManager.Play();
        }

        public void PlayMultiplierSound()
        {
            bonusSoundManager.clip = multiplierSound;
            bonusSoundManager.Play();
        }

        public void PlayAccelerationSound()
        {
            bonusSoundManager.clip = accelerationSound;
            bonusSoundManager.Play();
        }

        public void PlayShieldSound()
        {
            bonusSoundManager.clip = shieldSound;
            bonusSoundManager.Play();
        }
    }
}