using UnityEngine;

namespace Core.Bonuses
{
    public class Magnet : MonoBehaviour
    {
        private BonusController bonusController;

        public void Construct(BonusController _bonusController)
        {
            bonusController = _bonusController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponent<Player>().SetActiveMagnet(true);
            bonusController.ActivateBonus(BonusType.Magnet);
            
            Destroy(gameObject);
        }
    }
}