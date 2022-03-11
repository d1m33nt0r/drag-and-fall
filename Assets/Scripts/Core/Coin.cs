using UnityEngine;

namespace Core
{
    public class Coin : MonoBehaviour
    {
        private int count = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().CollectCoin(count);
                Destroy(gameObject);
            }
        }
    }
}