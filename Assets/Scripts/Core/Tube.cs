using System.Collections;
using Common;
using Data.Core;
using Data.Core.Segments;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    public class Tube : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private int countPlatforms;
        [SerializeField] private float distanceBetweenPlatforms;
        [SerializeField] private float platformMovementSpeed;
        [SerializeField] private DragController dragController;
        [SerializeField] public VisualController visualController;
        
        private float startTime;
        private float journeyLength;
        
        private PatternData currentPatternData;
        private Platform[] platforms;
        private Vector3[] localPositionsOfPlatforms;

        private void Awake()
        {
            dragController.SwipeEvent += RotateTube;
            Initialize();
            journeyLength = Vector3.Distance(platforms[0].transform.position, platforms[1].transform.position);
        }

        private void Initialize()
        {
            transform.GetComponent<MeshRenderer>().material = visualController.GetTubeMaterial();
        
            InitializePlatformPoints();
            InitializePlatforms();
        }

        private void RotateTube(DragController.SwipeType type, float delta)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            switch (type)
            {
                case DragController.SwipeType.LEFT:
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y  - delta, eulerAngles.z), 0.1f);
                    break;
                case DragController.SwipeType.RIGHT:
                    transform.DORotate(new Vector3(eulerAngles.x, eulerAngles.y  + delta, eulerAngles.z), 0.1f);
                    break;
            }
        }
        
        public void MovePlatforms()
        {
            startTime = Time.time;
            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            for (var i = 1; i < countPlatforms; i++)
                platforms[i - 1] = platforms[i];
            
            CreateNewPlatform(countPlatforms - 1);
            
            while (platforms[0].transform.position != localPositionsOfPlatforms[0])
            {
                for (var i = 1; i < countPlatforms; i++)
                {
                    float distCovered = (Time.time - startTime) * platformMovementSpeed;
                    float fractionOfJourney = distCovered / journeyLength;
                    platforms[i - 1].transform.position = Vector3.Lerp(localPositionsOfPlatforms[i], localPositionsOfPlatforms[i - 1], fractionOfJourney);
                }

                yield return null;
            }
        }

        private void InitializePlatformPoints()
        {
            localPositionsOfPlatforms = new Vector3[countPlatforms];
        
            for (var i = 0; i < countPlatforms; i++)
            {
                localPositionsOfPlatforms[i] = new Vector3(transform.position.x, transform.localPosition.y - distanceBetweenPlatforms * i, transform.position.z);
            }
        }
    
        private void InitializePlatforms()
        {
            platforms = new Platform[countPlatforms];
        
            for (var i = 0; i < countPlatforms; i++)
                CreateNewPlatform(i);
        }

        private void CreateNewPlatform(int _platformIndex)
        {
            var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.identity, transform);
        
            var patternData = new PatternData(Constants.Platform.COUNT_SEGMENTS, 0)
            {
                segmentsData = new SegmentData[]
                {
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Let },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Hole },
                    new SegmentData { segmentType = SegmentType.Hole },
                    new SegmentData { segmentType = SegmentType.Ground },
                    new SegmentData { segmentType = SegmentType.Let },
                    new SegmentData { segmentType = SegmentType.Ground }
                }
            };
        
            platformInstance.GetComponent<Platform>().Initialize(Constants.Platform.COUNT_SEGMENTS, patternData, this, player);
        
            platforms[_platformIndex] = platformInstance.GetComponent<Platform>();
        }
    }
}
