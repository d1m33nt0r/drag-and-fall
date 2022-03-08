using System;
using UnityEngine;

namespace Data.Shop.TubeSkins
{
    [Serializable]
    public class EnvironmentSkinData
    {
        public float price;
        public Skybox skybox;
        public Material tubeMaterial;
        public Material letSegmentMaterial;
        public Material groundSegmentMaterial;
        public Mesh segment;
    }
}