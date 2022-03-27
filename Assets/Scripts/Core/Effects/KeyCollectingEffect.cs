using ObjectPool;
using UnityEngine;

namespace Core.Effects
{
    public class KeyCollectingEffect : MonoBehaviour
    {
        private EffectsPool effectsPool;
        
        public void Construct(EffectsPool effectsPool)
        {
            this.effectsPool = effectsPool;
        }
        
        public void ReturnToPool()
        {
            effectsPool.ReturnKeyCollectingEffectToPool(this);
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}