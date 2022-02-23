using System;
using Data;
using Data.Core;

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
}