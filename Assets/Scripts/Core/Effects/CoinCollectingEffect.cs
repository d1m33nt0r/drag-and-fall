using ObjectPool;
using UnityEngine;

namespace Core.Effects
{
    public class CoinCollectingEffect : MonoBehaviour
    {
        private EffectsPool effectsPool;
        
        public void Construct(EffectsPool effectsPool)
        {
            this.effectsPool = effectsPool;
        }
        
        public void ReturnToPool()
        {
            //Destroy(gameObject);
            effectsPool.ReturnCoinCollectingEffectToPool(this);
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}