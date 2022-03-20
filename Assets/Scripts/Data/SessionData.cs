using UnityEngine;

namespace Data
{
    public class SessionData : MonoBehaviour
    {
        public int coins;
        public int crystals;
        public int keys;

        public void AddCoins(int count)
        {
            coins += count;
        }

        public void AddCrystals(int count)
        {
            crystals += count;
        }

        public void AddKeys(int count)
        {
            keys += count;
        }
        
        public void ResetData()
        {
            coins = 0;
            crystals = 0;
            keys = 0;
        }
    }
}