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

        public void SetRotation()
        {
            transform.rotation = Quaternion.identity;
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}