using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace ObjectPool
{
    public class TubePool : MonoBehaviour
    {
        [SerializeField] private GameObject tubePrefab;
        [SerializeField] private int poolSize;
        [SerializeField] private TubeMover tubeMover;
        
        private Queue<GameObject> tubePool = new Queue<GameObject>();
        
        public void Start()
        {
            for (var i = 0; i < poolSize; i++)
                tubePool.Enqueue(InstantiateNewPrefab());
        }

        private GameObject InstantiateNewPrefab()
        {
            var instance = Instantiate(tubePrefab, transform);
            instance.GetComponent<TubePart>().Initialize(tubeMover, this);
            instance.SetActive(false);
            return instance;
        }

        public GameObject GetTubePart()
        {
            if (tubePool.Count > 0)
            {
                var instance = tubePool.Dequeue();
                instance.gameObject.SetActive(true);
                return instance;
            }

            var instance2 = InstantiateNewPrefab();
            instance2.SetActive(true);
            return instance2;
        }

        public void ReturnToPool(GameObject obj)
        {
            tubePool.Enqueue(obj);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }
    }
}