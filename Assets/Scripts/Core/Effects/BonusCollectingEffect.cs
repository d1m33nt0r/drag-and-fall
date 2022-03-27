using ObjectPool;
using UnityEngine;

namespace Core.Effects
{
    public class BonusCollectingEffect : MonoBehaviour
    {
        private EffectsPool effectsPool;
        
        public void Construct(EffectsPool effectsPool)
        {
            this.effectsPool = effectsPool;
        }
        
        public void ReturnToPool()
        {
            //Destroy(gameObject);
            effectsPool.ReturnBonusCollectingEffectToPool(this);
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}