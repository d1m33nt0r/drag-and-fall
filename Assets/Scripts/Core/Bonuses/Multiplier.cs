using Data.Core.Segments.Content;
using ObjectPool;
using UnityEngine;

namespace Core.Bonuses
{
    public class Multiplier : MonoBehaviour
    {
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;
        
        public void Construct(BonusController _bonusController, SegmentContentPool segmentContentPool)
        {
            this.segmentContentPool = segmentContentPool;
            bonusController = _bonusController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            bonusController.ActivateBonus(BonusType.Multiplier);
            
            segmentContentPool.ReturnObjectToPool(SegmentContent.Multiplier, gameObject);
        }
    }
}