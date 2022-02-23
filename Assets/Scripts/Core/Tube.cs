using Core;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int countPlatforms;
    [SerializeField] private float distanceBetweenPlatforms;
    
    private Platform[] platforms;
    private Vector3[] localPositionsOfPlatforms;

    private void Start()
    {
        InitializePlatformPoints();
        InitializePlatforms();
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
        {
            var platformInstance = Instantiate(platformPrefab, localPositionsOfPlatforms[i], Quaternion.identity, transform);
            platforms[i] = platformInstance.GetComponent<Platform>();
        }
    }
    
    public void MovePlatforms()
    {
        Destroy(platforms[0]);
        
        for (var i = 1; i < countPlatforms; i++)
        {
            
        }
    }
}
