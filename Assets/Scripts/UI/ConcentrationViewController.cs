using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    [ExecuteAlways]
    public class ConcentrationViewController : MonoBehaviour
    {
        private int Animation = Animator.StringToHash("Concentration");
        [SerializeField] private Slider slider;
        [SerializeField] private Image x2Image;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Concentration concentration;
        [SerializeField] private AudioSource concentrationAudio;
        [SerializeField] private Animator concentrationAnimator;
        
        public void Start()
        {
            slider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
            UpdateMultiplierText(concentration.currentConcentrationMultiplier);
        }

        public void ValueChangeCheck()
        {
            if (slider.value == slider.maxValue)
            {
                x2Image.color = activeColor;
                concentrationAudio.Play();
                concentrationAnimator.Play(Animation);
            }
            else
            {
                x2Image.color = inactiveColor;
            }
        }

        public void UpdateMultiplierText(int multiplier)
        {
            text.text = "x" + multiplier;
        }
    }
}