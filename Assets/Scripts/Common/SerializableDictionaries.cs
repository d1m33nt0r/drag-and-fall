using System;
using Data.Core;
using Data.Shop.FallingTrails;
using Data.Shop.Players;
using Data.Shop.Trails;
using Data.Shop.TubeSkins;

namespace Common
{
    [Serializable]
    public class SerializableLevelData : SerializableDictionary<int, LevelData>
    {
    }
    
    [Serializable]
    public class SerializableInfinityData : SerializableDictionary<int, InfinityData>
    {
    }
    
    [Serializable]
    public class SerializableSetData : SerializableDictionary<int, SetData>
    {
    }

    [Serializable]
    public class SerializableTrailSkinData : SerializableDictionary<int, TrailSkinData>
    {
    }
    
    [Serializable]
    public class SerializableFallingTrailSkinData : SerializableDictionary<int, FallingTrailSkinData>
    {
    }
    
    [Serializable]
    public class SerializableEnvironmentSkinData : SerializableDictionary<int, EnvironmentSkinData>
    {
    }
    
    [Serializable]
    public class SerializablePlayerSkinData : SerializableDictionary<int, PlayerSkinData>
    {
    }
}