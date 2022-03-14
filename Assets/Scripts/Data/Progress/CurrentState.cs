using System;

namespace Progress
{
    [Serializable]
    public class CurrentState
    {
        public ShopItem environmentSkin;
        public ShopItem playerSkin;
        public ShopItem trailSkin;
        public ShopItem fallingTrailSkin;
        public CurrenciesProgress currenciesProgress;
    }
}