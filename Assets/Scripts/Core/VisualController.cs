using PatternManager;
using UnityEngine;

namespace Core
{
    public class VisualController : MonoBehaviour
    {
        [SerializeField] private Material[] segmentsMaterials;
        [SerializeField] private Material tubeMaterial;

        public Material GetSegmentMaterial(SegmentType _segmentType)
        {
            switch (_segmentType)
            {
                case SegmentType.Ground:
                    return segmentsMaterials[0];
                case SegmentType.Let:
                    return segmentsMaterials[1];
                default:
                    return null;
            }
        }

        public Material GetTubeMaterial()
        {
            return tubeMaterial;
        }
    }
}