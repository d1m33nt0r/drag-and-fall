using ObjectPool;
using UnityEngine;

namespace Core.Effects
{
    public class PlayerParticles : MonoBehaviour
    {
        private EffectsPool effectsPool;
        
        public void Construct(EffectsPool effectsPool)
        {
            this.effectsPool = effectsPool;
        }
        
        public void ReturnToPool()
        { 
            //Destroy(gameObject);
            effectsPool.ReturnParticlesToPool(this);
        }
        
        void OnParticleSystemStopped()
        {
            ReturnToPool();
        }
    }
}