using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace ObjectPool
{
    public class MainPool : MonoBehaviour
    {
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private GameObject segmentPrefab;

        [SerializeField] private int platformPoolSize;
        [SerializeField] private int segmentPoolSize;
        
        private Queue<GameObject> platforms = new Queue<GameObject>();
        private Queue<GameObject> segments = new Queue<GameObject>();

        private void Initialize()
        {
            for (var i = 0; i < platformPoolSize; i++)
            {
                var platformInstance = Instantiate(platformPrefab, transform);
                platformInstance.SetActive(false);
                platforms.Enqueue(platformInstance);
            }
            
            for (var i = 0; i < segmentPoolSize; i++)
            {
                var segmentInstance = Instantiate(segmentPrefab, transform);
                segmentInstance.SetActive(false);
                segments.Enqueue(segmentInstance);
            }
        }

        public GameObject GetCleanPlatformInstance()
        {
            if (platforms.Count > 0)
            {
                var instance = platforms.Dequeue();
                instance.SetActive(true);
                return instance;
            }
            
            platforms.Enqueue(Instantiate(platformPrefab, transform));
            return platforms.Dequeue();
        }
        
        public GameObject GetCleanSegmentInstance()
        {
            if (segments.Count > 0)
            {
                var instance = segments.Dequeue();
                instance.SetActive(true);
                return instance;
            }

            segments.Enqueue(Instantiate(segmentPrefab, transform));
            return segments.Dequeue();
        }

        public void ReturnPlatformToPool(GameObject gameObject)
        {
            platforms.Enqueue(gameObject);
            gameObject.transform.SetParent(transform);
            gameObject.SetActive(false);
        }
        
        public void ReturnSegmentToPool(GameObject gameObject)
        {
            segments.Enqueue(gameObject);
            gameObject.transform.SetParent(transform);
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            Initialize();
        } 
    }
}