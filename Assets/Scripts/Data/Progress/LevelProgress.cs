using System;

namespace Data.Progress
{
    [Serializable]
    public class LevelProgress
    {
        public bool isCompleted;
        public bool isUnlocked;
        public int countStars;
        public int countPoints;
        public int[] rewardIsIssued = new int[3];
        
    }
}