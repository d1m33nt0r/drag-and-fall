using System;
using System.Collections;
using Data;
using Data.Shop.TubeSkins;
using ObjectPool;
using UnityEngine;

namespace Core
{
    public class TubeMover : MonoBehaviour
    {
        private float distCovered;
        private float fractionOfJourney;
        private float speed;
        private bool isMoving;
        [SerializeField] private GameObject particleSystem;
        [SerializeField] private TubePool tubePool;
        [SerializeField] private GameObject tubePrefab;
        [SerializeField] private int countTubeParts;

        public TubePart[] tubeParts;
        public PlatformMover platformMover;
        public bool tubeIsInitialized;
        private Coroutine tubePartCoroutine;
        private Vector3[] localPositionsOfTubeParts;
        private float startTime;
        private float journeyLength;

        private void Awake()
        {
            Initialize();
            journeyLength = Vector3.Distance(localPositionsOfTubeParts[0], localPositionsOfTubeParts[1]);
        }

        private void Initialize()
        {
            if (particleSystem != null) Destroy(particleSystem);
            particleSystem = platformMover.visualController.GetBackgroundParticleSystem();
            InitializeTubePoints();
            InitializeTube();
        }

        private void InitializeTubePoints()
        {
            localPositionsOfTubeParts = new Vector3[countTubeParts];

            var j = 6f;
            for (var i = 0; i < countTubeParts; i++)
            {
                localPositionsOfTubeParts[i] = new Vector3(transform.position.x, transform.position.y + j, transform.position.z);
                j -= 1.5f;
            }
        }

        public void InitializeTube()
        {
            if (tubeIsInitialized)
            {
                for (var i = 0; i < countTubeParts; i++)
                {
                    tubeParts[i].GetComponent<TubePart>().ReturnToPool();
                }
            }

            tubeParts = new TubePart[countTubeParts];

            for (var i = 0; i < countTubeParts; i++)
            {
                var tubePartInstance = tubePool.GetTubePart();
                tubePartInstance.transform.position = localPositionsOfTubeParts[i];
                tubePartInstance.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
                tubePartInstance.transform.SetParent(transform);
                    
                tubeParts[i] = tubePartInstance.GetComponent<TubePart>();
                //AlignRotation(tubePartInstance);
            }

            tubeIsInitialized = true;
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            if (particleSystem != null) Destroy(particleSystem);
            particleSystem = Instantiate(_environmentSkinData.backgroundParticleSystem);
            //particleSystem.transform.position = _environmentSkinData.particleSystemPosition;
            
            foreach (var tubePart in tubeParts)
            {
                tubePart.TryOnSkin(_environmentSkinData);
            }
        }

        public void ChangeTheme()
        {
            if (particleSystem != null) Destroy(particleSystem);
            particleSystem = platformMover.visualController.GetBackgroundParticleSystem();
            
            foreach (var tubePart in tubeParts)
            {
                tubePart.ChangeTheme();
            }
            
            tubePool.ChangeTheme();
        }

        public void CreateNewTubePart()
        {
            tubeParts[0].ReturnToPool();
            
            for (var i = 1; i < countTubeParts; i++)
                tubeParts[i - 1] = tubeParts[i];

            var tubePartInstance = tubePool.GetTubePart();
            tubePartInstance.position = localPositionsOfTubeParts[localPositionsOfTubeParts.Length - 1];
            tubePartInstance.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            tubePartInstance.SetParent(transform);
            
            tubeParts[countTubeParts - 1] = tubePartInstance.GetComponent<TubePart>();
        }

        public void MoveTube(float speed)
        {
            startTime = Time.time;
            distCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distCovered / journeyLength;
            this.speed = speed;
            isMoving = true;
            CreateNewTubePart();
        }
        
        public void Update()
        {
            if (!isMoving) return;
            
           
            for (var i = 0; i < countTubeParts - 1; i++)
            {
                distCovered = (Time.time - startTime) * this.speed;
                fractionOfJourney = distCovered / journeyLength;
                tubeParts[i].transform.position =
                    Vector3.Lerp(localPositionsOfTubeParts[i + 1], localPositionsOfTubeParts[i], fractionOfJourney);
            }
            
        }
    }
}