﻿using UnityEngine;

namespace Core
{
    public class FreeSpeedIncrease : MonoBehaviour
    {
        [SerializeField] private PlatformMover platformMover;
        private float threshold = 6f;
        private int platformsThreshold = 3;
        public int currentPlatforms = 0;
        private float coefficientSglazhivanija = 0;

        public void IncreaseSpeed()
        {
            if (platformMover.platformMovementSpeed >= threshold) return;
           
            //platformMover.IncreaseSpeed(0.1f * currentPlatforms - coefficientSglazhivanija);
            platformMover.IncreaseSpeed(1);
            coefficientSglazhivanija += 0.025f * currentPlatforms / 1.5f;
            currentPlatforms++;
        }

        public void ResetSpeed()
        {
            currentPlatforms = 0;
            coefficientSglazhivanija = 0;
            platformMover.ResetDefaultSpeed();
        }
    }
}