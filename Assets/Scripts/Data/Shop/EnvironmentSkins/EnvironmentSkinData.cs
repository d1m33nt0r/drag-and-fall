using System;
using UnityEngine;

namespace Data.Shop.TubeSkins
{
    [Serializable]
    public class EnvironmentSkinData
    {
        public bool isBought;
        public float price;
        public Skybox skybox;
        public Material tubeMaterial;
        public Material letSegmentMaterial;
        public Material groundSegmentMaterial;
        public Mesh segment;
        public Mesh tube;
    }
}