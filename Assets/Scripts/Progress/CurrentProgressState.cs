using System;

namespace Progress
{
    [Serializable]
    public class CurrentProgressState
    {
        public ShopItem environmentSkin;
        public ShopItem playerSkin;
        public ShopItem trailSkin;
        public ShopItem fallingTrailSkin;
        public int coins;
    }
}