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
        
        private Queue<Transform> tubePool = new Queue<Transform>();
        
        public void Start()
        {
            for (var i = 0; i < poolSize; i++)
                tubePool.Enqueue(InstantiateNewPrefab().transform);
        }

        private GameObject InstantiateNewPrefab()
        {
            var instance = Instantiate(tubePrefab, transform);
            instance.GetComponent<TubePart>().Initialize(tubeMover, this);
            instance.SetActive(false);
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

            var instance2 = InstantiateNewPrefab();
            instance2.SetActive(true);
            return instance2.transform;
        }

        public void ChangeTheme(string themeIdentifier)
        {
            foreach (var obj in tubePool)
            {
                obj.GetComponent<TubePart>().ChangeTheme(themeIdentifier);
            }
        }

        public void ReturnToPool(Transform obj)
        {
            tubePool.Enqueue(obj);
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }
    }
}