using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace ObjectPool
{
    public class TubePool : MonoBehaviour
    {
        [SerializeField] private Transform tubePrefab;
        [SerializeField] private int poolSize;
        [SerializeField] private TubeMover tubeMover;
        
        private Queue<Transform> tubePool = new Queue<Transform>();
        
        public void Start()
        {
            for (var i = 0; i < poolSize; i++)
                tubePool.Enqueue(InstantiateNewPrefab(true));
        }

        private Transform InstantiateNewPrefab(bool isActivate)
        {
            var instance = Instantiate(tubePrefab, transform);
            instance.GetComponent<TubePart>().Initialize(tubeMover, this);
            instance.gameObject.SetActive(isActivate);
            return instance;
        }

        public Transform GetTubePart()
        {
            if (tubePool.Count > 0)
            {
                var instance = tubePool.Dequeue();
                instance.gameObject.SetActive(true);
                return instance;
            }

            var instance2 = InstantiateNewPrefab(true);
            return instance2.transform;
        }

        public void ChangeTheme()
        {
            foreach (var obj in tubePool)
            {
                obj.GetComponent<TubePart>().ChangeTheme();
            }
        }

        public void ReturnToPool(Transform obj)
        {
            tubePool.Enqueue(obj);
            obj.SetParent(transform);
            obj.gameObject.SetActive(false);
        }
    }
}