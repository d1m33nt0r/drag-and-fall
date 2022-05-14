using Common;
using Data;
using Data.Core.Segments;
using ObjectPool;
using Progress;
using UnityEngine;

namespace Core
{
    public class VisualController : MonoBehaviour
    {
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private PlatformPool platformPool;
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
            var themeIdentifier = progressController.currentState.environmentSkin.index.ToString() 
                                  + progressController.currentState.playerSkin.index;
            
            platformMover.ChangeTheme(themeIdentifier, progressController.currentState.environmentSkin.index);
            player.ChangeTheme(themeIdentifier);
            
            foreach (var platform in platformMover.platforms)
            {
                for (var i = 0; i < Constants.Platform.COUNT_SEGMENTS; i++)
                    platform.transform.GetChild(i).GetComponent<Segment>().ChangeTheme(themeIdentifier);
            }
            platformPool.ChangeTheme(themeIdentifier);
            /*Destroy(fireEffect);
            fireEffect = Instantiate(GetTrail(), new Vector3(0, 0.15f, -0.7f), Quaternion.identity, transform.parent);
            fireEffect.SetActive(false);*/
            segmentContentPool.ChangeTheme(platformMover.visualController.GetMaterial(themeIdentifier));
        }

        public GameObject GetBackgroundParticleSystem(int environmentThemeIndex)
        {
            var particles = Instantiate(shopData.EnvironmentSkinData[environmentThemeIndex]
                .backgroundParticleSystem);
            
            return particles;
        }

        public Material GetSkyboxMaterial(int environmentThemeIndex)
        {
            return shopData.EnvironmentSkinData[environmentThemeIndex].skybox;
        }

        public Mesh GetPlayerMesh()
        {
            return shopData.PlayerSkinData[progressController.currentState.playerSkin.index].mesh;
        }
        
        public Material GetMaterial(string identifier)
        {
            return mapOfSkins.Skin[identifier];
        }
        
        public Mesh[] GetPlatformColors()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].groundSegmentMeshes;
        }

        public Mesh GetTubeMesh()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].tube;
        }
        
        public Mesh GetSegmentMesh(SegmentType segmentType)
        {
            switch (segmentType)
            {
                case SegmentType.Ground:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segment;
                case SegmentType.Let:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].letSegmentMesh;
                default:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segment;
            }
            
        }
        
        public GameObject GetTrail()
        {
            return shopData.TrailSkinData[progressController.currentState.trailSkin.index].skin;
        }
    }
}