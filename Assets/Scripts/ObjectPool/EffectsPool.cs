using System.Collections.Generic;
using Core.Effects;
using UI;
using UnityEngine;

namespace ObjectPool
{
    public class EffectsPool : MonoBehaviour
    {

        /*[SerializeField] private GainScore gainScore;
        [SerializeField] private int gainScorePoolSize;*/

        [SerializeField] private PlayerParticles playerParticles;
        [SerializeField] private int particlesPoolSize;
        
        [SerializeField] private TouchEffect touchEffect;
        [SerializeField] private int touchPoolSize;

        [SerializeField] private BonusCollectingEffect bonusCollectingEffect;
        [SerializeField] private int bonusEffectPoolSize;

        [SerializeField] private CoinCollectingEffect coinCollectingEffect;
        [SerializeField] private int coinEffectPoolSize;

        [SerializeField] private CrystalCollectingEffect crystalCollectingEffect;
        [SerializeField] private int crystalEffectPoolSize;

        [SerializeField] private KeyCollectingEffect keyCollectingEffect;
        [SerializeField] private int keyEffectPoolSize;
        
        private Queue<GainScore> gainScorePool = new Queue<GainScore>();
        private Queue<PlayerParticles> particlesPool = new Queue<PlayerParticles>();
        private Queue<TouchEffect> touchPool = new Queue<TouchEffect>();
        private Queue<BonusCollectingEffect> bonusEffectPool = new Queue<BonusCollectingEffect>();
        private Queue<CoinCollectingEffect> coinEffectPool = new Queue<CoinCollectingEffect>();
        private Queue<CrystalCollectingEffect> crystalEffectPool = new Queue<CrystalCollectingEffect>();
        private Queue<KeyCollectingEffect> keyEffectPool = new Queue<KeyCollectingEffect>();
        
        private void Awake()
        {
            /*for (var i = 0; i < gainScorePoolSize; i++)
            {
                var instance = Instantiate(gainScore, transform);
                instance.gameObject.SetActive(false);
                instance.Construct(this);
                gainScorePool.Enqueue(gainScore);
            }*/
            
            for (var i = 0; i < particlesPoolSize; i++)
            {
                var instance = Instantiate(playerParticles, transform);
                instance.gameObject.SetActive(false);
                instance.Construct(this);
                particlesPool.Enqueue(instance);
            }
            
            for (var i = 0; i < touchPoolSize; i++)
            {
                var instance = Instantiate(touchEffect, transform);
                instance.gameObject.SetActive(false);
                instance.Construct(this);
                touchPool.Enqueue(instance);
            }
            
            for (var i = 0; i < bonusEffectPoolSize; i++)
            {
                var instance = Instantiate(bonusCollectingEffect, transform);
                instance.GetComponent<BonusCollectingEffect>().Construct(this);
                instance.gameObject.SetActive(false);
                bonusEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < coinEffectPoolSize; i++)
            {
                var instance = Instantiate(coinCollectingEffect, transform);
                instance.GetComponent<CoinCollectingEffect>().Construct(this);
                instance.gameObject.SetActive(false);
                coinEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < crystalEffectPoolSize; i++)
            {
                var instance = Instantiate(crystalCollectingEffect, transform);
                instance.GetComponent<CrystalCollectingEffect>().Construct(this);
                instance.gameObject.SetActive(false);
                crystalEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < keyEffectPoolSize; i++)
            {
                var instance = Instantiate(keyCollectingEffect, transform);
                instance.GetComponent<KeyCollectingEffect>().Construct(this);
                instance.gameObject.SetActive(false);
                keyEffectPool.Enqueue(instance);
            }
        }
        
        public void ReturnKeyCollectingEffectToPool(KeyCollectingEffect effect)
        {
            effect.transform.SetParent(transform);
            keyEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public KeyCollectingEffect GetKeyCollectingEffect()
        {
            if (keyEffectPool.Count > 0)
            {
                var effect = keyEffectPool.Dequeue();
                effect.gameObject.SetActive(true);
                effect.transform.SetParent(null);
                return effect;
            }
            
            var instance = Instantiate(keyCollectingEffect, transform);
            instance.GetComponent<KeyCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnBonusCollectingEffectToPool(BonusCollectingEffect effect)
        {
            effect.transform.SetParent(transform);
            bonusEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public BonusCollectingEffect GetBonusCollectingEffect()
        {
            if (bonusEffectPool.Count > 0)
            {
                var effect = bonusEffectPool.Dequeue();
                effect.gameObject.SetActive(true);
                effect.transform.SetParent(null);
                return effect;
            }
            
            var instance = Instantiate(bonusCollectingEffect, transform);
            instance.GetComponent<BonusCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnCoinCollectingEffectToPool(CoinCollectingEffect effect)
        {
            effect.transform.SetParent(transform);
            coinEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public CoinCollectingEffect GetCoinCollectingEffect()
        {
            if (coinEffectPool.Count > 0)
            {
                var effect = coinEffectPool.Dequeue();
                effect.gameObject.SetActive(true);
                effect.transform.SetParent(null);
                return effect;
            }
            
            var instance = Instantiate(coinCollectingEffect, transform);
            instance.GetComponent<CoinCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnCrystalCollectingEffectToPool(CrystalCollectingEffect effect)
        {
            effect.transform.SetParent(transform);
            crystalEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public CrystalCollectingEffect GetCrystalCollectingEffect()
        {
            if (crystalEffectPool.Count > 0)
            {
                var effect = crystalEffectPool.Dequeue();
                effect.gameObject.SetActive(true);
                effect.transform.SetParent(null);
                return effect;
            }
            
            var instance = Instantiate(crystalCollectingEffect, transform);
            instance.GetComponent<CrystalCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public TouchEffect GetTouchEffect()
        {
            if (touchPool.Count > 0)
            {
                var temp = touchPool.Dequeue();
                temp.gameObject.SetActive(true);
                return temp;
            }
            
            var instance = Instantiate(touchEffect, transform);
            instance.Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnObjectToPool(TouchEffect touchEffect)
        {
            touchEffect.transform.SetParent(transform);
            touchPool.Enqueue(touchEffect);
            touchEffect.gameObject.SetActive(false);
        }

        public PlayerParticles GetPlayerParticles()
        {
            if (particlesPool.Count > 0)
            {
                var temp = particlesPool.Dequeue();
                temp.gameObject.SetActive(true);
                return temp;
            }
            
            var instance = Instantiate(playerParticles, transform);
            instance.Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnParticlesToPool(PlayerParticles playerParticles)
        {
            playerParticles.transform.SetParent(transform);
            particlesPool.Enqueue(playerParticles);
            playerParticles.gameObject.SetActive(false);
        }
        
        /*public GainScore GetGainScoreEffect()
        {
            if (gainScorePool.Count > 0)
            {
                var temp = gainScorePool.Dequeue();
                temp.gameObject.SetActive(true);
                return temp;
            }
            
            var instance = Instantiate(gainScore, transform);
            instance.Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnGainScoreToPool(GainScore gainScore)
        {
            gainScore.transform.SetParent(transform);
            gainScorePool.Enqueue(gainScore);
            gainScore.gameObject.SetActive(false);
        }*/
    }
}