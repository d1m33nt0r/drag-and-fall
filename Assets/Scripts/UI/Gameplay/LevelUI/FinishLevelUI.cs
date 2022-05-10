using System.Collections;
using Core;
using Data.Core;
using Progress;
using Sound;
using Sound.UI;
using TMPro;
using UI.FinishLevel;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishLevelUI : MonoBehaviour
    {
        [SerializeField] private FinishLevelSounds finishLevelSounds;
        [SerializeField] private MainThemeSound mainThemeSound;
        
        [SerializeField] private TutorialUI tutorialUI;
        [SerializeField] private Transform tryAgainButtonTransform;
        [SerializeField] private Transform closeButtonTransform;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ProgressController progressController;
        [SerializeField] private UIManager uIManager;
        [SerializeField] private Text score;
        [SerializeField] private Slider scoreSlider;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private Animator animator;

        [SerializeField] private Animator firstStarAnimator;
        [SerializeField] private Animator secondStarAnimator;
        [SerializeField] private Animator thirdStarAnimator;
        [SerializeField] private Animator firstSlotAnimator;
        [SerializeField] private Animator secondSlotAnimator;
        [SerializeField] private Animator thirdSlotAnimator;

        [SerializeField] private FirstRewardSlot firstRewardSlot;
        [SerializeField] private SecondRewardSlot secondRewardSlot;
        [SerializeField] private ThirdRewardSlot thirdRewardSlot;

        private Coroutine scoreCoroutine;
        private int actualScoreValue;
        private int currentScoreValue;
        private LevelData levelData;

        private bool firstStarIsActive;
        private bool secondStarIsActive;
        private bool thirdStarIsActive;
      
        
        public void SetText(int score, int level)
        {
            this.level.text = "Level " + level;
            actualScoreValue = score;
        }

        public void SetLevelData(LevelData levelData)
        {
            this.levelData = levelData;
            firstRewardSlot.SetLevelData(levelData);
            secondRewardSlot.SetLevelData(levelData);
            thirdRewardSlot.SetLevelData(levelData);
        }

        public void PlayShowAnimation()
        {
            animator.Play("Show");
        }

        public void ShowTutorialUI()
        {
            if (!tutorialUI.fourthStepComplete)
                tutorialUI.ShowFourthStep();
        }
        
        public void PlayHideAnimation()
        {
            if (tutorialUI.firstGeneralStepComplete)
                animator.Play("Hide");
            else
            {
                tutorialUI.transform.GetChild(3).gameObject.SetActive(false);
                animator.Play("TutorialHide");
            }

            //tutorialUI.firstGeneralStepComplete = true;
            
        }

        public void ShowLevels()
        {
            ResetDefaultState();
            gameManager.ShowLevels();
        }
        
        public void ShowMainMenu()
        {
            ResetDefaultState();
            gameManager.ShowMainMenu();
        }
        
        public void ResetDefaultState()
        {
            scoreSlider.value = 0;
            score.text = "0";
            firstStarIsActive = false;
            secondStarIsActive = false;
            thirdStarIsActive = false;
            firstStarAnimator.transform.GetChild(0).localScale = Vector3.zero;
            secondStarAnimator.transform.GetChild(0).localScale = Vector3.zero;
            thirdStarAnimator.transform.GetChild(0).localScale = Vector3.zero;
            tryAgainButtonTransform.transform.localScale = Vector3.zero;
            closeButtonTransform.transform.localScale = Vector3.zero;
            firstSlotAnimator.GetComponent<FirstRewardSlot>().SetDefaultState();
            secondSlotAnimator.GetComponent<SecondRewardSlot>().SetDefaultState();
            thirdSlotAnimator.GetComponent<ThirdRewardSlot>().SetDefaultState();
        }
        
        public void AnimateScore()
        {
            if (scoreCoroutine != null) StopCoroutine(scoreCoroutine);
            scoreCoroutine = StartCoroutine(ScoreAnimation());
        }

        public void SetMaxValue()
        {
            scoreSlider.maxValue = levelData.bestScore;
        }

        private IEnumerator ScoreAnimation()
        {
            mainThemeSound.Stop();
            var isFinish = false;
            var t = 0f;
            yield return new WaitForSeconds(0.5f);
            finishLevelSounds.PlayPointCounter();
            while (true)
            {
                var value = (int) Mathf.Lerp(currentScoreValue, actualScoreValue, t);
                
                if (value >= levelData.bottomScore && !firstStarIsActive)
                {
                    firstStarAnimator.Play("Show");
                    finishLevelSounds.PlayStarSound();
                    firstStarIsActive = true;
                    if (isFinish) break;
                }
                
                if (value >= levelData.middleScore && !secondStarIsActive)
                {
                    secondStarAnimator.Play("Show");
                    finishLevelSounds.PlayStarSound();
                    secondStarIsActive = true;
                    if (isFinish) break;
                }
                
                if (value >= levelData.bestScore && !thirdStarIsActive)
                {
                    thirdStarAnimator.Play("Show");
                    finishLevelSounds.PlayStarSound();
                    thirdStarIsActive = true;
                    if (isFinish) break;
                }

                score.text = value.ToString();
                scoreSlider.value = Mathf.Lerp(currentScoreValue, actualScoreValue, t);
                if (isFinish) break;
                t += 0.5f * Time.deltaTime;
                if (t > 1.0f) isFinish = true;
                yield return null;
            }

            if (firstStarIsActive && secondStarIsActive && thirdStarIsActive)
            {
                if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars < 3)
                {
                    progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars = 3;
                    uIManager.UpdateLevelsStatus();
                }
            }
            
            if (firstStarIsActive && secondStarIsActive && !thirdStarIsActive)
            {
                if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars < 2)
                {
                    progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars = 2;
                    uIManager.UpdateLevelsStatus();
                }
            }
            
            if (firstStarIsActive && !secondStarIsActive && !thirdStarIsActive)
            {
                if (progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars < 1)
                {
                    progressController.levelsProgress.levelsProgresses[levelData.levelIndex].countStars = 1;
                    uIManager.UpdateLevelsStatus();
                }
            }

            finishLevelSounds.StopPointCounter();

            yield return new WaitForSeconds(1f);
            if (firstStarIsActive)
            {
                finishLevelSounds.PlayLevelBonusSound();
                firstSlotAnimator.Play("Show");
            }
            yield return new WaitForSeconds(0.75f);
            if (secondStarIsActive)
            {
                finishLevelSounds.PlayLevelBonusSound();
                secondSlotAnimator.Play("Show");
            }
            yield return new WaitForSeconds(0.75f);
            if (thirdStarIsActive)
            {
                finishLevelSounds.PlayLevelBonusSound();
                thirdSlotAnimator.Play("Show");
            }

            yield return new WaitForSeconds(0.75f);
            animator.Play("ShowButtons");
        }
    }
}