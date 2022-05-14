using Common;
using Data;
using Data.Core.Segments;
using Data.Shop.TubeSkins;
using ObjectPool;
using Progress;
using UnityEngine;

namespace Core
{
    public class VisualController : MonoBehaviour
    {
        public MapOfSkins mapOfSkins;
        [SerializeField] private ShopData shopData;
        [SerializeField] private ProgressController progressController;
        
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private Player player;

        [SerializeField] private SegmentContentPool segmentContentPool;
        
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

            segmentContentPool.ChangeTheme(platformMover.visualController.GetTubeMaterial());
        }

        public GameObject GetBackgroundParticleSystem()
        {
            var particles = Instantiate(shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index]
                .backgroundParticleSystem);
            
            return particles;
        }
        
        public Material GetSegmentMaterial(SegmentType _segmentType)
        {
            var str = progressController.currentState.environmentSkin.index.ToString() +
                      progressController.currentState.playerSkin.index;
            
            return mapOfSkins.Skin[str];
        }
        
        public Material TryOnSegmentMaterial(EnvironmentSkinData environmentSkinData)
        {
            var str = environmentSkinData.index.ToString() + progressController.currentState.playerSkin.index;
            
            return mapOfSkins.Skin[str];
        }

        public Material GetSkyboxMaterial()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].skybox;
        }

        public Material GetPlayerMaterial()
        {
            var str = progressController.currentState.environmentSkin.index.ToString() +
                      progressController.currentState.playerSkin.index;
            
            return mapOfSkins.Skin[str];
        }

        public Mesh GetPlayerMesh()
        {
            return shopData.PlayerSkinData[progressController.currentState.playerSkin.index].mesh;
        }
        
        public Material GetTubeMaterial()
        {
            var str = progressController.currentState.environmentSkin.index.ToString() +
                      progressController.currentState.playerSkin.index;
            
            return mapOfSkins.Skin[str];
        }
        
        public Mesh[] GetPlatformColors()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].groundSegmentMeshes;
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