using System;
using UnityEngine;

namespace Data.Shop.Players
{
    [Serializable]
    public class PlayerSkinData
    {
        public bool isBought;
        public float price;
        public Mesh mesh;
        public Material material;
    }
}