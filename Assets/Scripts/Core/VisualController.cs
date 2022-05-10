using Common;
using Data;
using Data.Core.Segments;
using Progress;
using UnityEngine;

namespace Core
{
    public class VisualController : MonoBehaviour
    {
        [SerializeField] private ShopData shopData;
        [SerializeField] private ProgressController progressController;
        
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;

        private void Start()
        {
            SetNeededThemes();
        }

        public void SetNeededThemes()
        {
            platformMover.ChangeTheme();
            player.ChangeTheme();
            
            foreach (var platform in platformMover.platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().ChangeTheme();
            }
        }

        public GameObject GetBackgroundParticleSystem()
        {
            var particles = Instantiate(shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index]
                .backgroundParticleSystem);
            
            return particles;
        }
        
        public Material GetSegmentMaterial(SegmentType _segmentType)
        {
            switch (_segmentType)
            {
                case SegmentType.Ground:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segmentMaterial;
                case SegmentType.Let:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segmentMaterial;
                default:
                    return null;
            }
        }

        public Material GetSkyboxMaterial()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].skybox;
        }

        public Material GetPlayerMaterial()
        {
            return shopData.PlayerSkinData[progressController.currentState.playerSkin.index].material;
        }

        public Mesh GetPlayerMesh()
        {
            return shopData.PlayerSkinData[progressController.currentState.playerSkin.index].mesh;
        }
        
        public Material GetTubeMaterial()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].tubeMaterial;
        }
        
        public Mesh[] GetPlatformColors()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].meshes;
        }

        public Mesh GetTubeMesh()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].tube;
        }
        
        public GameObject GetTrail()
        {
            return shopData.TrailSkinData[progressController.currentState.trailSkin.index].skin;
        }
        
        public Mesh GetSegmentMesh(SegmentType segmentType)
        {
            if (segmentType == SegmentType.Ground)
                return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segment;
            if (segmentType == SegmentType.Let)
                return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index]
                    .letSegmentMesh;

            return null;
        }
    }
}