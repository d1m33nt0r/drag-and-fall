using Data.Shop.TubeSkins;
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
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            RenderSettings.skybox = _environmentSkinData.skybox;
            meshRenderer.material = _environmentSkinData.tubeMaterial;
            meshFilter.mesh = _environmentSkinData.tube;
        }
        
        public void ChangeTheme()
        {
            RenderSettings.skybox = tubeMover.platformMover.visualController.GetSkyboxMaterial();
            meshRenderer.material = tubeMover.platformMover.visualController.GetTubeMaterial();
            meshFilter.mesh = tubeMover.platformMover.visualController.GetTubeMesh();
        }

        public void ReturnToPool()
        {
            tubePool.ReturnToPool(mtransform);
        }
    }
}