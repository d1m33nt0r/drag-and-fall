using UnityEngine;

namespace UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject levelProgressUI;
        [SerializeField] private GameObject levelFinishIcon;
        [SerializeField] private GameObject levelFillArea;
        [SerializeField] private GameObject levelLevelBackground;
        [SerializeField] private GameObject levelText;
        [SerializeField] private GameObject levelSlider;


        [SerializeField] private GameObject infMode1;
        [SerializeField] private GameObject infMode2;
        [SerializeField] private GameObject infMode3;
        [SerializeField] private GameObject infMode4;
        [SerializeField] private GameObject infMode5;
        [SerializeField] private GameObject infMode6;
        [SerializeField] private GameObject infMode7;
        [SerializeField] private GameObject infMode8;
        
        public void EnableInfinityMode()
        {
            levelProgressUI.SetActive(false);
            levelFinishIcon.SetActive(false);
            levelFillArea.SetActive(false);
            levelLevelBackground.SetActive(false);
            levelText.SetActive(false);
            levelSlider.SetActive(false);
            
            infMode1.SetActive(true);
            infMode2.SetActive(true);
            infMode3.SetActive(true);
            infMode4.SetActive(true);
            infMode5.SetActive(true);
            infMode6.SetActive(true);
            infMode7.SetActive(true);
            infMode8.SetActive(true);
        }

        public void EnableLevelMode()
        {
            levelProgressUI.SetActive(true);
            levelFinishIcon.SetActive(true);
            levelFillArea.SetActive(true);
            levelLevelBackground.SetActive(true);
            levelText.SetActive(true);
            levelSlider.SetActive(true);
            
            infMode1.SetActive(true);
            infMode2.SetActive(true);
            infMode3.SetActive(true);
            infMode4.SetActive(true);
            infMode5.SetActive(true);
            infMode6.SetActive(true);
            infMode7.SetActive(true);
            infMode8.SetActive(true);
        }

        public void DisableGameplayMode()
        {
            levelProgressUI.SetActive(false);
            levelFinishIcon.SetActive(false);
            levelFillArea.SetActive(false);
            levelLevelBackground.SetActive(false);
            levelText.SetActive(false);
            levelSlider.SetActive(false);
            
            infMode1.SetActive(false);
            infMode2.SetActive(false);
            infMode3.SetActive(false);
            infMode4.SetActive(false);
            infMode5.SetActive(false);
            infMode6.SetActive(false);
            infMode7.SetActive(false);
            infMode8.SetActive(false);
        }
    }
}