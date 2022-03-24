using Data.Core.Segments.Content;
using ObjectPool;
using UnityEngine;

namespace Core.Bonuses
{
    public class Magnet : MonoBehaviour
    {
        private BonusController bonusController;
        private SegmentContentPool segmentContentPool;
        
        public void Construct(BonusController _bonusController, SegmentContentPool segmentContentPool)
        {
            bonusController = _bonusController;
            this.segmentContentPool = segmentContentPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            other.GetComponent<Player>().SetActiveMagnet(true);
            bonusController.ActivateBonus(BonusType.Magnet);

            segmentContentPool.ReturnObjectToPool(SegmentContent.Magnet, gameObject);
        }
    }
}