using System;
using UnityEngine;

namespace Data.Shop.TubeSkins
{
    [Serializable]
    public class EnvironmentSkinData
    {
        public bool isBought;
        public float priceCoins;
        public float priceCrystals;
        public Material skybox;
        public Material tubeMaterial;
        public Material letSegmentMaterial;
        public Material groundSegmentMaterial;
        public Mesh segment;
        public Mesh tube;
        public Color[] platformColors;
        public Mesh letSegmentMesh;
        public Material segmentMaterial;
        public Mesh[] meshes;
        public GameObject backgroundParticleSystem;
        public Vector3 particleSystemPosition;
    }
}