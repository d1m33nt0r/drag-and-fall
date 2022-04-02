using System;
using System.Collections;
using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class TubeMover : MonoBehaviour
    {
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
                    Destroy(tubeParts[i].gameObject);
                }
            }

            tubeParts = new TubePart[countTubeParts];

            for (var i = 0; i < countTubeParts; i++)
            {
                var tubePartInstance = Instantiate(tubePrefab, localPositionsOfTubeParts[i],
                    Quaternion.Euler(transform.rotation.eulerAngles),
                    transform);
                tubePartInstance.GetComponent<TubePart>().Initialize(this);
                tubeParts[i] = tubePartInstance.GetComponent<TubePart>();
                //AlignRotation(tubePartInstance);
            }

            tubeIsInitialized = true;
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            foreach (var tubePart in tubeParts)
            {
                tubePart.TryOnSkin(_environmentSkinData);
            }
        }

        public void ChangeTheme()
        {
            foreach (var tubePart in tubeParts)
            {
                tubePart.ChangeTheme();
            }
        }

        public void CreateNewTubePart()
        {
            Destroy(tubeParts[0].gameObject);
            
            for (var i = 1; i < countTubeParts; i++)
                tubeParts[i - 1] = tubeParts[i];

            var tubePartInstance = Instantiate(tubePrefab,
                localPositionsOfTubeParts[localPositionsOfTubeParts.Length - 1],
                Quaternion.Euler(transform.rotation.eulerAngles), transform);

            tubePartInstance.GetComponent<TubePart>().Initialize(this);
            tubeParts[countTubeParts - 1] = tubePartInstance.GetComponent<TubePart>();
        }

        public void MoveTube()
        {
            startTime = Time.time;
            if (tubePartCoroutine != null)
            {
                StopCoroutine(tubePartCoroutine);
            }
            
            CreateNewTubePart();
            
            tubePartCoroutine = StartCoroutine(MovingTube(platformMover.platformMovementSpeed));
        }
        
        public IEnumerator MovingTube(float speed)
        {
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            
            while (distCovered / journeyLength != 1)
            {
                for (var i = 0; i < countTubeParts - 1; i++)
                {
                    distCovered = (Time.time - startTime) * speed;
                    fractionOfJourney = distCovered / journeyLength;
                    tubeParts[i].transform.position =
                        Vector3.Lerp(localPositionsOfTubeParts[i + 1], localPositionsOfTubeParts[i], fractionOfJourney);
                }

                yield return null;
            }
        }
    }
}