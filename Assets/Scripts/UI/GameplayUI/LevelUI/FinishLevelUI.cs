using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishLevelUI : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text level;
        
        public void SetText(int score, int level)
        {
            this.level.text = "Level " + level;
            this.score.text = score.ToString();
        }
    }
}