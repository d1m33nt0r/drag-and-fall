using System;
using UnityEngine;

namespace Data.Shop.Players
{
    [Serializable]
    public class PlayerSkinData
    {
        public bool isBought;
        public float priceCoins;
        public float priceCrystals;
        public Mesh mesh;
        public Material material;
    }
}