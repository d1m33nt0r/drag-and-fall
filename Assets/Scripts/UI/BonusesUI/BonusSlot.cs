using System;
using UnityEngine;

namespace UI.Bonuses
{
    [ExecuteAlways]
    public class BonusSlot : MonoBehaviour
    {
        
       
        
        public int index;
        private RectTransform rectTransform;
        public bool contained => rectTransform.childCount > 0;
        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        
        public void SetUp(BonusView bonusView)
        {
            //bonusView.GetComponent<RectTransform>().SetParent(rectTransform);
            //bonusView.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            //bonusView.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        }

        private void Update()
        {
            
        }

        public void SetDown(BonusView bonusView)
        {
            bonusView.GetComponent<RectTransform>().SetParent(transform.parent);
        }
    }
}