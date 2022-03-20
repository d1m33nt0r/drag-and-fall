using Core;
using Data.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Tube tube;
        
        private int currentCountPlatforms;
        private int countPlatforms;
        private int currentFinishPercent => currentCountPlatforms / countPlatforms;

        public void Initialize(LevelData levelData)
        {
            countPlatforms = levelData.patterns.Count;
            currentCountPlatforms = 0;
            slider.value = 0;
            slider.maxValue = 1;
        }

        public void Step()
        {
            if (!tube.isLevelMode) return;
            currentCountPlatforms += 1;
            slider.value = currentFinishPercent;
        }
    }
}