using System;
using UnityEngine;

namespace Data.Shop.Players
{
    [Serializable]
    public class PlayerSkinData
    {
        public int skinId;
        public float price;
        public GameObject skin;
    }
}