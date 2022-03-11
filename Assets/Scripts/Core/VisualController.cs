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
        [SerializeField] private CurrentProgressState currentProgressState;
        
        [SerializeField] private Tube tube;
        [SerializeField] private Player player;

        private void Start()
        {
            SetNeededThemes();
        }

        public void SetNeededThemes()
        {
            tube.ChangeTheme();
            player.ChangeTheme();
            
            foreach (var platform in tube.platforms)
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
                    return shopData.EnvironmentSkinData[currentProgressState.environmentSkin.index]
                        .groundSegmentMaterial;
                case SegmentType.Let:
                    return shopData.EnvironmentSkinData[currentProgressState.environmentSkin.index].letSegmentMaterial;
                default:
                    return null;
            }
        }

        public Material GetPlayerMaterial()
        {
            return shopData.PlayerSkinData[currentProgressState.playerSkin.index].material;
        }

        public Mesh GetPlayerMesh()
        {
            return shopData.PlayerSkinData[currentProgressState.playerSkin.index].mesh;
        }
        
        public Material GetTubeMaterial()
        {
            return shopData.EnvironmentSkinData[currentProgressState.environmentSkin.index].tubeMaterial;
        }

        public Mesh GetTubeMesh()
        {
            return shopData.EnvironmentSkinData[currentProgressState.environmentSkin.index].tube;
        }
        
        public Mesh GetSegmentMesh()
        {
            return shopData.EnvironmentSkinData[currentProgressState.environmentSkin.index].segment;
        }
        
        public GameObject GetTrail()
        {
            return shopData.TrailSkinData[currentProgressState.trailSkin.index].skin;
        }

        public GameObject GetFallingTrail()
        {
            return shopData.FallingTrailSkinData[currentProgressState.fallingTrailSkin.index].skin;
        }
    }
}