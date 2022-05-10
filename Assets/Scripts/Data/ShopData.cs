using System.Collections.Generic;
using Data.Shop.FallingTrails;
using Data.Shop.Players;
using Data.Shop.Trails;
using Data.Shop.TubeSkins;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "Gamer Stash/Shop Data", order = 0)]
    public class ShopData : ScriptableObject
    {
        public List<EnvironmentSkinData> EnvironmentSkinData;
        public List<PlayerSkinData> PlayerSkinData;
        public List<TrailSkinData> TrailSkinData;
    }
}