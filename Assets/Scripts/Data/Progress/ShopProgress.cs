using System;
using System.Collections.Generic;

namespace Progress
{
    [Serializable]
    public class ShopProgress
    {
        public List<ShopItem> environmentSkins;
        public List<ShopItem> playerSkins;
        public List<ShopItem> trailSkins;
    }

    [Serializable]
    public class ShopItem
    {
        public int index;
        public bool isBought;
    }
}