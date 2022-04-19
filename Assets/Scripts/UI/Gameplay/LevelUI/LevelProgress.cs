using Core;
using Data.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Text level;
        
        public void Initialize(LevelData levelData)
        {
            level.text = (levelData.levelIndex + 1).ToString();
            slider.value = 0;
            slider.minValue = 0;
            slider.maxValue = levelData.patterns.Count;
        }

        public void ResetProgressValue()
        {
            slider.minValue = 0;
            slider.value = 0;
        }
        
        public void Step()
        {
            if (!platformMover.isLevelMode) return;
            slider.value += 1;
        }
    }
}