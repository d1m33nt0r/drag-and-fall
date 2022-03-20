using UnityEngine;

namespace Core.Bonuses
{
    public class Key : MonoBehaviour
    {
        private BonusController bonusController;

        public void Construct(BonusController _bonusController)
        {
            bonusController = _bonusController;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().CollectKey(1);
                Destroy(gameObject);
            }
        }
    }
}