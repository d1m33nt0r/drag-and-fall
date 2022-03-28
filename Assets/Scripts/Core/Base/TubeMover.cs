using System;
using System.Collections;
using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class TubeMover : MonoBehaviour
    {
        [SerializeField] private GameObject tubePrefab;
        [SerializeField] private float distanceBetweenTubeParts;
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

            var j = 4;
            for (var i = 0; i < countTubeParts; i++)
            {
                localPositionsOfTubeParts[i] = new Vector3(transform.position.x, transform.localPosition.y + j,
                    transform.position.z);
                j -= 2;
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
            for (var i = 1; i < countTubeParts; i++)
                tubeParts[i - 1] = tubeParts[i];

            var tubePartInstance = Instantiate(tubePrefab,
                tubeParts[tubeParts.Length - 2].transform.position - Vector3.down * 2,
                Quaternion.Euler(transform.rotation.eulerAngles), transform);

            tubePartInstance.GetComponent<TubePart>().Initialize(this);
            tubeParts[tubeParts.Length - 1] = tubePartInstance.GetComponent<TubePart>();
            //AlignRotation(tubePartInstance);
        }

        public void MoveTube()
        {
            startTime = Time.time;
            if (tubePartCoroutine != null) StopCoroutine(tubePartCoroutine);
            //tubePartCoroutine = StartCoroutine(MovingTube(platformMover.platformMovementSpeed));
        }

        public IEnumerator MovingTube(float speed)
        {
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;

            var targetPositions = new Vector3[countTubeParts];
            var currentPositions = new Vector3[countTubeParts];

            for (var i = 0; i < countTubeParts; i++)
            {
                currentPositions[i] = tubeParts[i].transform.position;
                targetPositions[i] = currentPositions[i] + new Vector3(currentPositions[i].x,
                    currentPositions[i].y + 2, currentPositions[i].z);
            }

            while (distCovered / journeyLength != 1)
            {
                for (var i = 0; i < countTubeParts; i++)
                {
                    distCovered = (Time.time - startTime) * speed;
                    fractionOfJourney = distCovered / journeyLength;
                    tubeParts[i].transform.position =
                        Vector3.Lerp(currentPositions[i], targetPositions[i], fractionOfJourney);
                }

                yield return null;
            }
        }
    }
}