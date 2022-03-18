using System;

namespace Data.Progress
{
    [Serializable]
    public class UpgradeProgress
    {
        public bool[] progressMagnet;
        public bool[] progressShield;
        public bool[] progressAcceleration;
        public bool[] progressMultiplier;
    }
}