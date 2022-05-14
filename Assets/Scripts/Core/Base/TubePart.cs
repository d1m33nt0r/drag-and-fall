using ObjectPool;
using UnityEngine;

namespace Core
{
    public class TubePart : MonoBehaviour
    {
        private TubeMover tubeMover;
        private TubePool tubePool;
        private Transform mtransform;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(TubeMover _tubeMover, TubePool tubePool)
        {
            mtransform = GetComponent<Transform>();
            tubeMover = _tubeMover;
            this.tubePool = tubePool;
        }
        
        
        public void ChangeTheme(string themeIdentifier)
        {
            meshRenderer.material = tubeMover.platformMover.visualController.GetMaterial(themeIdentifier);
            meshFilter.mesh = tubeMover.platformMover.visualController.GetTubeMesh();
        }

        public void ReturnToPool()
        {
            tubePool.ReturnToPool(mtransform);
        }
    }
}