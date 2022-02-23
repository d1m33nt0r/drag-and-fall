using Common;
using Data.Core;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Gamer Stash/Game Data", order = 0)]
    public class GameData : ScriptableObject
    {
        public InfinityData infinityData;
        public SerializableLevelData levelDatas;
        public SerializableInfinityData infinityDatas;
        public SerializableDictionary<int, SetData> setDatas;
    }
}