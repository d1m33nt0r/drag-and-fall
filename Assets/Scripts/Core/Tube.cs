using System.Collections;
using Core;
using DG.Tweening;
using PatternManager;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int countPlatforms;
    [SerializeField] private float distanceBetweenPlatforms;
    [SerializeField] private float platformMovementSpeed;
    
    [SerializeField] public VisualController visualController;
    
    private PatternData currentPatternData;
    private Platform[] platforms;
    private Vector3[] localPositionsOfPlatforms;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        transform.GetComponent<MeshRenderer>().material = visualController.GetTubeMaterial();
        
        InitializePlatformPoints();
        InitializePlatforms();
        
        StartCoroutine(SimulatePlatformsMoving());
    }
    
    public void MovePlatforms()
    {
        Destroy(platforms[0].gameObject);
        
        for (var i = 1; i < countPlatforms; i++)
        {
            platforms[i - 1] = platforms[i];
            platforms[i - 1].transform.DOLocalMoveY(localPositionsOfPlatforms[i - 1].y, platformMovementSpeed);
        }
        
        CreateNewPlatform(countPlatforms - 1);
    }
    
    private IEnumerator SimulatePlatformsMoving()
    {
        while (true)
        {
            MovePlatforms();
            
            yield return new WaitForSeconds(1);
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
        
        var patternData = new PatternData(12, 0)
        {
            segmentsData = new SegmentData[]
            {
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Let },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Let },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Ground },
                new SegmentData { segmentType = SegmentType.Let },
                new SegmentData { segmentType = SegmentType.Ground }
                
            }
        };
        
        platformInstance.GetComponent<Platform>().Initialize(12, patternData, this);
        
        platforms[_platformIndex] = platformInstance.GetComponent<Platform>();
    }
}
