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
        
        public void Initialize(LevelData levelData)
        {
            slider.value = 0;
            slider.maxValue = levelData.patterns.Count;
        }

        public void Step()
        {
            if (!tube.isLevelMode) return;
            slider.value += 1;
        }
    }
}