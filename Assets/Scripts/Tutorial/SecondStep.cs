using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class SecondStep : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Text text2;
        [SerializeField] private GameManager gameManager;

        public void ChangeText()
        {
            if (text2.enabled && text.enabled == false)
            {
                gameObject.SetActive(false);
                gameManager.gameStarted = true;
            }
            
            if (text.enabled)
            {
                text.enabled = false;
                text2.enabled = true;
            }
        }
    }
}