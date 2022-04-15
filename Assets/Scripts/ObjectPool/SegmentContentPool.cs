using System.Collections.Generic;
using Core;
using Core.Bonuses;
using Data.Core.Segments.Content;
using Sound;
using UnityEngine;

namespace ObjectPool
{
    public class SegmentContentPool : MonoBehaviour
    {
        [SerializeField] private BonusSoundManager bonusSoundManager;
        [SerializeField] private CoinSound coinSound;
        [SerializeField] private CrystalSound crystalSound;
        
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject crystalPrefab;
        [SerializeField] private GameObject multiplier;
        [SerializeField] private GameObject shield;
        [SerializeField] private GameObject acceleration;
        [SerializeField] private GameObject magnet;
        [SerializeField] private GameObject key;

        [SerializeField] private int coinPoolSize;
        [SerializeField] private int crystalPoolSize;
        [SerializeField] private int multiplierPoolSize;
        [SerializeField] private int shieldPoolSize;
        [SerializeField] private int accelerationPoolSize;
        [SerializeField] private int magnetPoolSize;
        [SerializeField] private int keyPoolSize;

        private Dictionary<SegmentContent, Queue<GameObject>> pool = new Dictionary<SegmentContent, Queue<GameObject>>();

        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            InitializeCoinPool();
            InitializeCrystalPool();
            InitializeMultiplierPool();
            InitializeShieldPool();
            InitializeAccelerationPool();
            InitializeMagnetPool();
            InitializeKeyPool();
        }

        private void InitializeKeyPool()
        {
            pool.Add(SegmentContent.Key, new Queue<GameObject>());
            for (var i = 0; i < keyPoolSize; i++)
            {
                var instance = Instantiate(key, Vector3.zero, Quaternion.identity, transform);
                pool[SegmentContent.Key].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeMagnetPool()
        {
            pool.Add(SegmentContent.Magnet, new Queue<GameObject>());
            for (var i = 0; i < magnetPoolSize; i++)
            {
                var instance = Instantiate(magnet, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Magnet>().BindAudio(bonusSoundManager);
                pool[SegmentContent.Magnet].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeAccelerationPool()
        {
            pool.Add(SegmentContent.Acceleration, new Queue<GameObject>());
            for (var i = 0; i < accelerationPoolSize; i++)
            {
                var instance = Instantiate(acceleration, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Acceleration>().BindAudio(bonusSoundManager);
                pool[SegmentContent.Acceleration].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeShieldPool()
        {
            pool.Add(SegmentContent.Shield, new Queue<GameObject>());
            for (var i = 0; i < shieldPoolSize; i++)
            {
                var instance = Instantiate(shield, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Shield>().BindAudio(bonusSoundManager);
                pool[SegmentContent.Shield].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeMultiplierPool()
        {
            pool.Add(SegmentContent.Multiplier, new Queue<GameObject>());
            for (var i = 0; i < multiplierPoolSize; i++)
            {
                var instance = Instantiate(multiplier, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Multiplier>().BindAudio(bonusSoundManager);
                pool[SegmentContent.Multiplier].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeCrystalPool()
        {
            pool.Add(SegmentContent.Crystal, new Queue<GameObject>());
            for (var i = 0; i < crystalPoolSize; i++)
            {
                var instance = Instantiate(crystalPrefab, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Crystal>().BindAudio(crystalSound);
                pool[SegmentContent.Crystal].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }
        private void InitializeCoinPool()
        {
            pool.Add(SegmentContent.Coin, new Queue<GameObject>());
            for (var i = 0; i < coinPoolSize; i++)
            {
                var instance = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity, transform);
                instance.GetComponent<Coin>().BindAudio(coinSound);
                pool[SegmentContent.Coin].Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
        }

        public void ReturnObjectToPool(SegmentContent segmentContent, GameObject gameObject)
        {
            gameObject.transform.SetParent(transform);
            pool[segmentContent].Enqueue(gameObject);
            gameObject.SetActive(false);
        }

        public GameObject GetObject(SegmentContent segmentContent)
        {
            if (pool[segmentContent].Count > 0)
            {
                var obj = pool[segmentContent].Dequeue();
                obj.SetActive(true);
                return obj;
            }

            switch (segmentContent)
            {
                case SegmentContent.Acceleration:
                    var instance4 = Instantiate(acceleration, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance4);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Shield:
                    var instance3 = Instantiate(shield, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance3);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Crystal:
                    var instance2 = Instantiate(crystalPrefab, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance2);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Coin:
                    var instance = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Key:
                    var instance5 = Instantiate(key, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance5);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Magnet:
                    var instance6 = Instantiate(magnet, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance6);
                    return pool[segmentContent].Dequeue();
                case SegmentContent.Multiplier:
                    var instance7 = Instantiate(multiplier, Vector3.zero, Quaternion.identity, transform);
                    pool[segmentContent].Enqueue(instance7);
                    return pool[segmentContent].Dequeue();
                default:
                    return default;
            }
        }
    }
}