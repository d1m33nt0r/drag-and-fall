using UnityEngine;

namespace UI.InfinityUI
{
    public class BestScoreUI : MonoBehaviour
    {
        [SerializeField] private FailedInfinityUI failedInfinityUI;

        public void ShowStatisticsUI()
        {
            failedInfinityUI.ShowStatisticsUI();
        }
    }
}