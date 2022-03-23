using System.IO;
using Data.Core;
using Data.Progress;
using UnityEngine;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        [SerializeField] private LevelsData levelsData;
        
        public ShopProgress shopProgress;
        public CurrentState currentState;
        public LevelsProgress levelsProgress;
        public UpgradeProgress upgradeProgress;
        
        private string path;
        private string path2;
        private string path3;
        private string path4;

        private void Awake()
        {
#if UNITY_EDITOR
            path = Path.Combine(Application.dataPath, "shopProgress.json");
            path2 = Path.Combine(Application.dataPath, "currentState.json");
            path3 = Path.Combine(Application.dataPath, "levelsProgress.json");
            path4 = Path.Combine(Application.dataPath, "upgradeProgress.json");
#elif UNITY_ANDROID
            path = Path.Combine(Application.persistentDataPath, "shopProgress.json");
            path2 = Path.Combine(Application.persistentDataPath, "currentState.json");
            path3 = Path.Combine(Application.persistentDataPath, "levelsProgress.json");
            path4 = Path.Combine(Application.persistentDataPath, "upgradeProgress.json");
#endif
            for (var i = 0; i < levelsData.leves.Count; i++)
            {
                if (i == 0)
                {
                    levelsProgress.levelsProgresses.Add(new LevelProgress()
                    {
                        countPoints = 0,
                        countStars = 0,
                        isCompleted = false,
                        isUnlocked = true
                    });
                }
                else
                {
                    levelsProgress.levelsProgresses.Add(new LevelProgress()
                    {
                        countPoints = 0,
                        countStars = 0,
                        isCompleted = false,
                        isUnlocked = false
                    });
                }
            }
            
            var json = JsonUtility.ToJson(shopProgress);
            var json2 = JsonUtility.ToJson(currentState);
            var json3 = JsonUtility.ToJson(levelsProgress);
            var json4 = JsonUtility.ToJson(upgradeProgress);
            
            if (!File.Exists(path))
            {
                File.WriteAllText(path, json);
            }

            if (!File.Exists(path2))
            {
                File.WriteAllText(path2, json2);
            }

            if (!File.Exists(path3))
            {
                File.WriteAllText(path3, json3);
            }
            
            if (!File.Exists(path4))
            {
                File.WriteAllText(path4, json4);
            }
            
            shopProgress = JsonUtility.FromJson<ShopProgress>(File.ReadAllText(path));
            currentState = JsonUtility.FromJson<CurrentState>(File.ReadAllText(path2));
            levelsProgress = JsonUtility.FromJson<LevelsProgress>(File.ReadAllText(path3));
            upgradeProgress = JsonUtility.FromJson<UpgradeProgress>(File.ReadAllText(path4));
        }

        public void SaveShopData(ShopProgress _shopProgress)
        {
            shopProgress = _shopProgress;
            var json = JsonUtility.ToJson(shopProgress);
            File.WriteAllText(path, json);
        }

        public void SaveCurrentState(CurrentState _currentState)
        {
            currentState = _currentState;
            var json = JsonUtility.ToJson(currentState);
            File.WriteAllText(path2, json);
        }
        
        public void SaveLevelsProgress(LevelsProgress _levelsProgress)
        {
            levelsProgress = _levelsProgress;
            var json = JsonUtility.ToJson(levelsProgress);
            File.WriteAllText(path3, json);
        }

        public void SaveUpgradeProgress(UpgradeProgress upgradeProgress)
        {
            this.upgradeProgress = upgradeProgress;
            var json = JsonUtility.ToJson(this.upgradeProgress);
            File.WriteAllText(path4, json);
        }
    }
}