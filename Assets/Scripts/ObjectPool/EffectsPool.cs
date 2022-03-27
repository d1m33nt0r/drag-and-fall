using System.Collections.Generic;
using Core.Effects;
using UnityEngine;

namespace ObjectPool
{
    public class EffectsPool : MonoBehaviour
    {
        [SerializeField] private TouchEffect touchEffect;
        [SerializeField] private int touchPoolSize;

        [SerializeField] private GameObject bonusCollectingEffect;
        [SerializeField] private int bonusEffectPoolSize;

        [SerializeField] private GameObject coinCollectingEffect;
        [SerializeField] private int coinEffectPoolSize;

        [SerializeField] private GameObject crystalCollectingEffect;
        [SerializeField] private int crystalEffectPoolSize;

        [SerializeField] private GameObject keyCollectingEffect;
        [SerializeField] private int keyEffectPoolSize;
        
        private Queue<TouchEffect> touchPool = new Queue<TouchEffect>();
        private Queue<GameObject> bonusEffectPool = new Queue<GameObject>();
        private Queue<GameObject> coinEffectPool = new Queue<GameObject>();
        private Queue<GameObject> crystalEffectPool = new Queue<GameObject>();
        private Queue<GameObject> keyEffectPool = new Queue<GameObject>();
        
        private void Awake()
        {
            for (var i = 0; i < touchPoolSize; i++)
            {
                var instance = Instantiate(touchEffect, transform);
                instance.Construct(this);
                touchPool.Enqueue(instance);
            }
            
            for (var i = 0; i < bonusEffectPoolSize; i++)
            {
                var instance = Instantiate(bonusCollectingEffect, transform);
                instance.GetComponent<BonusCollectingEffect>().Construct(this);
                instance.SetActive(false);
                bonusEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < coinEffectPoolSize; i++)
            {
                var instance = Instantiate(coinCollectingEffect, transform);
                instance.GetComponent<CoinCollectingEffect>().Construct(this);
                instance.SetActive(false);
                coinEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < crystalEffectPoolSize; i++)
            {
                var instance = Instantiate(crystalCollectingEffect, transform);
                instance.GetComponent<CrystalCollectingEffect>().Construct(this);
                instance.SetActive(false);
                crystalEffectPool.Enqueue(instance);
            }
            
            for (var i = 0; i < keyEffectPoolSize; i++)
            {
                var instance = Instantiate(keyCollectingEffect, transform);
                instance.GetComponent<KeyCollectingEffect>().Construct(this);
                instance.SetActive(false);
                keyEffectPool.Enqueue(instance);
            }
        }
        
        public void ReturnKeyCollectingEffectToPool(GameObject effect)
        {
            effect.transform.SetParent(transform);
            keyEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public GameObject GetKeyCollectingEffect()
        {
            if (keyEffectPool.Count > 0) return keyEffectPool.Dequeue();
            
            var instance = Instantiate(keyCollectingEffect, transform);
            instance.GetComponent<KeyCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnBonusCollectingEffectToPool(GameObject effect)
        {
            effect.transform.SetParent(transform);
            bonusEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public GameObject GetBonusCollectingEffect()
        {
            if (bonusEffectPool.Count > 0) return bonusEffectPool.Dequeue();
            
            var instance = Instantiate(bonusCollectingEffect, transform);
            instance.GetComponent<BonusCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnCoinCollectingEffectToPool(GameObject effect)
        {
            effect.transform.SetParent(transform);
            coinEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public GameObject GetCoinCollectingEffect()
        {
            if (coinEffectPool.Count > 0) return coinEffectPool.Dequeue();
            
            var instance = Instantiate(coinCollectingEffect, transform);
            instance.GetComponent<CoinCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public void ReturnCrystalCollectingEffectToPool(GameObject effect)
        {
            effect.transform.SetParent(transform);
            crystalEffectPool.Enqueue(effect);
            effect.gameObject.SetActive(false);
        }

        public GameObject GetCrystalCollectingEffect()
        {
            if (crystalEffectPool.Count > 0) return crystalEffectPool.Dequeue();
            
            var instance = Instantiate(crystalCollectingEffect, transform);
            instance.GetComponent<CrystalCollectingEffect>().Construct(this);
            instance.gameObject.SetActive(true);
            return instance;
        }
        
        public TouchEffect GetTouchEffect()
        {
            if (touchPool.Count > 0) return touchPool.Dequeue();
            
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
    }
}