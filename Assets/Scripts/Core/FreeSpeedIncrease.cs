using UnityEngine;

namespace Core
{
    public class FreeSpeedIncrease : MonoBehaviour
    {
        [SerializeField] private Tube tube;
        private float threshold = 3.5f;
        
        public void IncreaseSpeed()
        {
            if (tube.platformMovementSpeed >= threshold) return;
            tube.IncreaseSpeed(0.1f);
        }

        public void ResetSpeed()
        {
            tube.ResetDefaultSpeed();
        }
    }
}