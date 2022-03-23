using System.Collections.Generic;
using Data.Core;
using Progress;
using UnityEngine;

namespace Core
{
    public class LevelUIController : MonoBehaviour
    {
        public PlatformMover platformMover; 
        public ProgressController progressController;
        public LevelsData levelsData;
        [SerializeField] private List<LevelButton> levelButtons;

        private int countPages => progressController.levelsProgress.levelsProgresses.Count % 20;

        private void Start()
        {
            InitializeLevels();
        }

        public void InitializeLevels()
        {
            for (var i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].SetLevelIndex(i);
                levelButtons[i].SetLevelText((i + 1).ToString());
                
                if (progressController.levelsProgress.levelsProgresses.Count - 1 < i)
                {
                    levelButtons[i].gameObject.SetActive(false);
                    continue;
                }

                if (!progressController.levelsProgress.levelsProgresses[i].isUnlocked)
                {
                    levelButtons[i].SetLockState();
                    continue;
                }
                    
                if (progressController.levelsProgress.levelsProgresses[i].isUnlocked && 
                    !progressController.levelsProgress.levelsProgresses[i].isCompleted)
                {
                    levelButtons[i].SetUnlockState();
                    continue;
                }
                    
                if (progressController.levelsProgress.levelsProgresses[i].isUnlocked && 
                    progressController.levelsProgress.levelsProgresses[i].isCompleted)
                {
                    levelButtons[i].SetCompletedState(progressController.levelsProgress.levelsProgresses[i].countStars);
                }
            }
        }
    }
}