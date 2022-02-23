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
        public Material letMaterial;
        public Material groundMaterial;
        public Mesh segment;
    }
}