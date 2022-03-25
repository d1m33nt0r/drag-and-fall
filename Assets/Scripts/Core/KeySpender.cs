using Progress;
using UnityEngine;

namespace Core
{
    public class KeySpender : MonoBehaviour
    {
        [SerializeField] private ProgressController progressController;
        [SerializeField] private KeyPanel keyPanel;
        [SerializeField] private GameManager gameManager;
        
        public int currentUsageStep = 1;
        private bool start = true;
        
        public void TrySpendKeys()
        {
            if (start && progressController.currentState.currenciesProgress.keys >= currentUsageStep)
            {
                keyPanel.MinusKeys(currentUsageStep);
                gameManager.ContinueGameKeys();
                start = false;
                currentUsageStep *= 2;
                return;
            }
            
            if (progressController.currentState.currenciesProgress.keys >= currentUsageStep)
            {
                keyPanel.MinusKeys(currentUsageStep);
                gameManager.ContinueGameKeys();
                currentUsageStep *= 2;
            }
        }

        public void ResetUsages()
        {
            start = true;
            currentUsageStep = 1;
        }
    }
}