﻿using Core;
using Data.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgress : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Tube tube;
        [SerializeField] private Text level;
        
        public void Initialize(LevelData levelData)
        {
            level.text = (levelData.levelIndex + 1).ToString();
            slider.value = 0;
            slider.maxValue = levelData.patterns.Count;
        }

        public void ResetProgressValue()
        {
            slider.value = 0;
        }
        
        public void Step()
        {
            if (!tube.isLevelMode) return;
            slider.value += 1;
        }
    }
}