using UnityEngine;

namespace Core.Bonuses
{
    public class Shield : MonoBehaviour
    {
        private BonusController bonusController;

        public void Construct(BonusController _bonusController)
        {
            bonusController = _bonusController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            bonusController.ActivateBonus(BonusType.Shield);
            
            Destroy(gameObject);
        }
    }
}