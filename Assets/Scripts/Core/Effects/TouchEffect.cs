using ObjectPool;
using UnityEngine;

namespace Core.Effects
{
    public class TouchEffect : MonoBehaviour
    {
        private EffectsPool effectsPool;
        
        public void Construct(EffectsPool effectsPool)
        {
            this.effectsPool = effectsPool;
        }
        
        public void ReturnToPool()
        { 
            //Destroy(gameObject);
            effectsPool.ReturnObjectToPool(this);
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}