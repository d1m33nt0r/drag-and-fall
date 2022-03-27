using Data.Core.Segments.Content;
using ObjectPool;
using UnityEngine;

namespace Core.Bonuses
{
    public class Key : MonoBehaviour
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
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().CollectKey(1);
                other.GetComponent<Player>().SpawnKeyCollectingEffect();
                segmentContentPool.ReturnObjectToPool(SegmentContent.Key, gameObject);
            }
        }
    }
}