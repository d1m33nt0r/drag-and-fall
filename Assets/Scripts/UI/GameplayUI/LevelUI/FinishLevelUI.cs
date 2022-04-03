using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishLevelUI : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text level;
        [SerializeField] private GameObject oneStar;
        [SerializeField] private GameObject twoStar;
        [SerializeField] private GameObject threeStar;
        [SerializeField] private Animator animator;
        
        public void SetText(int score, int level)
        {
            this.level.text = "Level " + level;
            this.score.text = score.ToString();
        }

        public void ShowThreeStars()
        {
            oneStar.SetActive(false);
            twoStar.SetActive(false);
            threeStar.SetActive(true);
        }
        public void ShowTwoStars()
        {
            oneStar.SetActive(false);
            twoStar.SetActive(true);
            threeStar.SetActive(false);
        }
        public void ShowOneStar()
        {
            oneStar.SetActive(true);
            twoStar.SetActive(false);
            threeStar.SetActive(false);
        }
    }
}