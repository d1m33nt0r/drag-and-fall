using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class TubePart : MonoBehaviour
    {
        private TubeMover tubeMover; 
        
        public void Initialize(TubeMover _tubeMover)
        {
            tubeMover = _tubeMover;
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            RenderSettings.skybox = _environmentSkinData.skybox;
            GetComponent<MeshRenderer>().material = _environmentSkinData.tubeMaterial;
            GetComponent<MeshFilter>().mesh = _environmentSkinData.tube;
        }
        
        public void ChangeTheme()
        {
            RenderSettings.skybox = tubeMover.platformMover.visualController.GetSkyboxMaterial();
            GetComponent<MeshRenderer>().material = tubeMover.platformMover.visualController.GetTubeMaterial();
            GetComponent<MeshFilter>().mesh = tubeMover.platformMover.visualController.GetTubeMesh();
        }
    }
}