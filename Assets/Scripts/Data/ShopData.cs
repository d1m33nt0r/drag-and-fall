using Common;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ShopData", menuName = "Gamer Stash/Shop Data", order = 0)]
    public class ShopData : ScriptableObject
    {
        public SerializableEnvironmentSkinData EnvironmentSkinData;
        public SerializablePlayerSkinData PlayerSkinData;
        public SerializableTrailSkinData TrailSkinData;
        public SerializableFallingTrailSkinData FallingTrailSkinData;
    }
}