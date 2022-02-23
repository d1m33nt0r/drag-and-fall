using System.Collections;
using Core;
using DG.Tweening;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int countPlatforms;
    [SerializeField] private float distanceBetweenPlatforms;
    [SerializeField] private float platformMovementSpeed;
    
    private Platform[] platforms;
    private Vector3[] localPositionsOfPlatforms;

    private void Start()
    {
        InitializePlatformPoints();
        InitializePlatforms();

        StartCoroutine(SimulatePlatformsMoving());
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

    private void CreateNewPlatform(int _platformIndex)
    {
        var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[_platformIndex], Quaternion.identity, transform);
        platforms[_platformIndex] = platformInstance.GetComponent<Platform>();
    }
}
