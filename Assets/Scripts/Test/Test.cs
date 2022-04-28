using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Transform[] temp;
        
        public void Update()
        {
            for (var i = 0; i < temp.Length; i++)
            {
                temp[i].gameObject.SetActive(!temp[i].gameObject.activeSelf);
            }
        }
    }
}