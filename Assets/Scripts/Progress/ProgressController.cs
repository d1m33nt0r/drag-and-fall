using System.IO;
using UnityEngine;

namespace Progress
{
    public class ProgressController : MonoBehaviour
    {
        public ShopProgress shopProgress;
        private string path;
        private string jsonData;
        
        private void Start()
        {
            path = Path.Combine(Application.dataPath, "shopProgress.json");
            var json = JsonUtility.ToJson(shopProgress);
            if (!File.Exists(path)) File.WriteAllText(path, json);
            shopProgress = JsonUtility.FromJson<ShopProgress>(File.ReadAllText(path));
        }

        public void SaveData(ShopProgress _shopProgress)
        {
            shopProgress = _shopProgress;
            var json = JsonUtility.ToJson(shopProgress);
            File.WriteAllText(path, json);
        }
    }
}