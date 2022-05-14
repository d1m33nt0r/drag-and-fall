using System;
using Data.Shop.TubeSkins;
using ObjectPool;
using Progress;
using UnityEngine;

namespace Core
{
    public class TubeMover : MonoBehaviour
    {
        public ProgressController progressController;
        
        [SerializeField] private GameObject particleSystem;
        [SerializeField] private TubePool tubePool;
        [SerializeField] private GameObject tubePrefab;
        [SerializeField] private int countTubeParts;

        private bool isMoving;
        public TubePart[] tubeParts;
        public PlatformMover platformMover;
        public bool tubeIsInitialized;
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
                    tubeParts[i].GetComponent<TubePart>().ReturnToPool();
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

        public void ChangeTheme(string themeIdentifier, int environmentThemeIndex)
        {
            if (particleSystem != null) Destroy(particleSystem);
            RenderSettings.skybox = platformMover.visualController.GetSkyboxMaterial(environmentThemeIndex);
            particleSystem = platformMover.visualController.GetBackgroundParticleSystem(environmentThemeIndex);
            
            foreach (var tubePart in tubeParts)
                tubePart.ChangeTheme(themeIdentifier);

            tubePool.ChangeTheme(themeIdentifier);
        }

        public void CreateNewTubePart()
        {
            tubeParts[0].ReturnToPool();
            
            for (var i = 1; i < countTubeParts; i++)
                tubeParts[i - 1] = tubeParts[i];

            var tubePartInstance = tubePool.GetTubePart();
            var tTransform = tubePartInstance.transform;
            tTransform.position = localPositionsOfTubeParts[localPositionsOfTubeParts.Length - 1];
            tTransform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
            tTransform.SetParent(transform);
            
            tubeParts[countTubeParts - 1] = tubePartInstance.GetComponent<TubePart>();
        }

        public void MoveTube()
        {
            startTime = Time.time;
            CreateNewTubePart();
            isMoving = true;
        }

        private void Update()
        {
            var distCovered = (Time.time - startTime) * platformMover.platformMovementSpeed;
            if (!isMoving) return;
            float fractionOfJourney = default;

            for (var i = 0; i < countTubeParts - 1; i++)
            {
                distCovered = (Time.time - startTime) * platformMover.platformMovementSpeed;
                fractionOfJourney = distCovered / journeyLength;
                tubeParts[i].transform.position =
                    Vector3.Lerp(localPositionsOfTubeParts[i + 1], localPositionsOfTubeParts[i], fractionOfJourney);
            }

            if (fractionOfJourney >= 1) isMoving = false;
        }
    }
}