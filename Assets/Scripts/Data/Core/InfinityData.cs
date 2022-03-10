using System.Collections.Generic;
using UnityEngine;

namespace Data.Core
{
    [CreateAssetMenu(fileName = "InfinityData", menuName = "Gamer Stash/Infinity Data", order = 0)]
    public class InfinityData : ScriptableObject
    {
        public List<SetData> patternSets;
    }
}