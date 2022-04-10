using Core;
using UnityEngine;

namespace ObjectPool
{
    public class PlatformPool : MonoBehaviour
    {
        [SerializeField] private Platform platformPrefab;
        [SerializeField] private int poolSize;
    }
}