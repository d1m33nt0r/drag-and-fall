using UnityEngine;

namespace Core.Bonuses
{
    public class Multiplier : MonoBehaviour
    {
        private BonusController bonusController;

        public void Construct(BonusController _bonusController)
        {
            bonusController = _bonusController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            bonusController.ActivateBonus(BonusType.Multiplier);
            
            Destroy(gameObject);
        }
    }
}