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

        public Material GetSegmentMaterial(SegmentType _segmentType)
        {
            switch (_segmentType)
            {
                case SegmentType.Ground:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index]
                        .groundSegmentMaterial;
                case SegmentType.Let:
                    return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].letSegmentMaterial;
                default:
                    return null;
            }
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
        
        public Color[] GetPlatformColors()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].platformColors;
        }

        public Mesh GetTubeMesh()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].tube;
        }
        
        public Mesh GetSegmentMesh()
        {
            return shopData.EnvironmentSkinData[progressController.currentState.environmentSkin.index].segment;
        }
        
        public GameObject GetTrail()
        {
            return shopData.TrailSkinData[progressController.currentState.trailSkin.index].skin;
        }

        public GameObject GetFallingTrail()
        {
            return shopData.FallingTrailSkinData[progressController.currentState.fallingTrailSkin.index].skin;
        }
    }
}