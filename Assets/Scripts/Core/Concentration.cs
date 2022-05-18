using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Concentration : MonoBehaviour
    {
        [SerializeField] private ProgressController progressController;
        [SerializeField] private ConcentrationViewController concentrationViewController;
        [SerializeField] private TutorialUI tutorialUI;
        public bool tutorialConcentrationComplete;
        public int currentConcentrationLevel = 1;
        public int currentConcentrationMultiplier
        {
            get
            {
                if (currentConcentrationLevel == 9)
                    return 6;
                if (currentConcentrationLevel >= 7)
                    return 5;
                if (currentConcentrationLevel >= 5)
                    return 4;
                if (currentConcentrationLevel >= 3)
                    return 3;
                
                return 2;
            }
        }

        public int currentConcentrationPlatformsUpgrade
        {
            get
            {
                if (currentConcentrationLevel >= 10)
                    return 6;
                if (currentConcentrationLevel >= 8)
                    return 5;
                if (currentConcentrationLevel >= 6)
                    return 4;
                if (currentConcentrationLevel >= 4)
                    return 3;
                if (currentConcentrationLevel >= 2)
                    return 2;
                
                return 1;
            }
        }

        private void Awake()
        {
            UpdateLevel();
        }

        public void UpdateLevel()
        {
            for (var i = 0; i < progressController.upgradeProgress.progressConcentration.Length; i++)
            {
                if (progressController.upgradeProgress.progressConcentration[i])
                    currentConcentrationLevel = i + 1;
            }

            if (tutorialUI.tutorialIsFinished)
                slider.maxValue = 16 - currentConcentrationPlatformsUpgrade;
            else
                slider.maxValue = 6;
            
            concentrationViewController.UpdateMultiplierText(currentConcentrationMultiplier);
        }

        public bool isActive
        {
            get
            {
                if (slider.value == 6) tutorialConcentrationComplete = true;
                if (tutorialUI.tutorialIsFinished)
                    return platformCounter > 16 - currentConcentrationPlatformsUpgrade;
                else
                    return slider.value == 6;
            }
        }

        public Slider slider;
        public int platformCounter { get; private set; }

        public void IncreaseConcentration()
        {
            platformCounter++;
            slider.value = platformCounter;
            if (isActive)
            {
                if (!tutorialUI.thirdStepComplete)
                {
                    tutorialUI.ShowThirdStep();
                }
            }
        }

        public void Reset()
        {
            platformCounter = 0;
            slider.value = platformCounter;
        }
    }
}