using UnityEngine;

namespace Core
{
    public class FreeSpeedIncrease : MonoBehaviour
    {
        [SerializeField] private PlatformMover platformMover;
        private float threshold = 3.5f;
        
        public void IncreaseSpeed()
        {
            if (platformMover.platformMovementSpeed >= threshold) return;
            platformMover.IncreaseSpeed(0.1f);
        }

        public void ResetSpeed()
        {
            platformMover.ResetDefaultSpeed();
        }
    }
}