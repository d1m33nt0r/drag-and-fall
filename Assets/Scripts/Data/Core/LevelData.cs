using System.Collections.Generic;
using UnityEngine;

namespace Data.Core
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Gamer Stash/Level Data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public List<PatternData> patterns;
    }
}