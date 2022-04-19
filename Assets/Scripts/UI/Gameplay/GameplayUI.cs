using UnityEngine;

namespace UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject levelProgressUI;

        public void EnableInfinityMode()
        {
            levelProgressUI.SetActive(false);
        }

        public void EnableLevelMode()
        {
            levelProgressUI.SetActive(true);
        }
    }
}