using System.IO;
using UnityEngine;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        public ShopProgress shopProgress;
        public CurrentState currentState;
        
        private string path;
        private string path2;

        private void Start()
        {
            path = Path.Combine(Application.dataPath, "shopProgress.json");
            path2 = Path.Combine(Application.dataPath, "currentState.json");
            
            var json = JsonUtility.ToJson(shopProgress);
            var json2 = JsonUtility.ToJson(currentState);
            
            if (!File.Exists(path)) File.WriteAllText(path, json);
            if (!File.Exists(path2)) File.WriteAllText(path2, json2);
            
            shopProgress = JsonUtility.FromJson<ShopProgress>(File.ReadAllText(path));
            currentState = JsonUtility.FromJson<CurrentState>(File.ReadAllText(path2));
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
    }
}