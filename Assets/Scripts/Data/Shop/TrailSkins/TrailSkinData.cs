using System;
using UnityEngine;

namespace Data.Shop.Trails
{
    [Serializable]
    public class TrailSkinData
    {
        public bool isBought;
        public float priceCoins;
        public float priceCrystals;
        public GameObject skin;
    }
}