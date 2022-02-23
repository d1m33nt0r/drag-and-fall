using System;
using Common;


namespace ObjectPool
{
    [Serializable]
    public class SerializablePool : SerializableDictionary<string, Pool>
    {
        
    }
}