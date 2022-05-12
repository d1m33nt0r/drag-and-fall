using System;
using UnityEngine;

namespace Data.Shop.TubeSkins
{
    [Serializable]
    public class EnvironmentSkinData
    {
        public int index;
        public bool isBought;
        public float priceCoins;
        public float priceCrystals;
        public Material skybox;
        public Mesh segment;
        public Mesh tube;
        public Mesh letSegmentMesh;
        public Mesh[] groundSegmentMeshes;
        public GameObject backgroundParticleSystem;
        public Vector3 particleSystemPosition;
    }
}