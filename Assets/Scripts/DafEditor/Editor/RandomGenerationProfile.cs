using UnityEngine;

namespace DafEditor.Editor
{
    [CreateAssetMenu(fileName = "RandomGenerationProfile", menuName = "DAFEditor/RandomGenerationProfile", order = 0)]
    public class RandomGenerationProfile : ScriptableObject
    {
        public int countPlatforms;
        public int maxLetAmount;
        public int minLetAmount;
        public int maxHoleAmount;
        public int minHoleAmount;
    }
}