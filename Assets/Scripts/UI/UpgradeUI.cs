using UnityEngine;

namespace Core
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameManager gameManager;
        
        public void ShowUpgradeUI()
        {
            animator.Play("Show");
        }

        public void HideUpgradeUI()
        {
            animator.Play("Hide");
        }

        public void ShowMainMenu()
        {
            gameManager.ShowMainMenu();
        }
    }
}