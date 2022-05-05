using Core;
using UnityEngine;

namespace Common
{
    public class PowerUpRotationAligner : MonoBehaviour
    {
        public void RotateTransform(DragController.SwipeType type, float delta)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            switch (type)
            {
                case DragController.SwipeType.LEFT:
                    transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + delta * 40 * Time.deltaTime, eulerAngles.z);
                    break;
                case DragController.SwipeType.RIGHT:
                    transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y - delta * 40 * Time.deltaTime, eulerAngles.z);
                    break;
            }
        }
    }
}