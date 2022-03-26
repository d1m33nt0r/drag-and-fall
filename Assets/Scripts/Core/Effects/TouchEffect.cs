using UnityEngine;

namespace Core.Effects
{
    public class TouchEffect : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}