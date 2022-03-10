using UnityEngine;

namespace Core
{
    public class GameMode : MonoBehaviour
    {
        public LevelMode levelMode;
        public InfinityMode infinityMode;

        public void ActiveInfinityMode()
        {
            levelMode.gameObject.SetActive(false);
            infinityMode.gameObject.SetActive(true);
        }

        public void ActiveLevelMode()
        {
            levelMode.gameObject.SetActive(true);
            infinityMode.gameObject.SetActive(false);
        }
    }
}