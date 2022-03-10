using System.Collections.Generic;
using UnityEngine;

namespace Data.Core
{
    [CreateAssetMenu(fileName = "SetData", menuName = "Gamer Stash/Set Data", order = 0)]
    public class SetData : ScriptableObject
    {
        public List<PatternData> patterns;
    }
}