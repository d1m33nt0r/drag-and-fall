using UnityEngine;

namespace Core
{
    public class Crystal : MonoBehaviour
    {
        private int count = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().CollectCrystal(count);
                Destroy(gameObject);
            }
        }
    }
}