using System;
using UnityEngine;

namespace Common
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField, Range(-1, 1)] private int direction;
        [SerializeField] private float speed;
        [SerializeField] private Axis axis;

        private void Update()
        {
            switch (axis)
            {
                case Axis.X:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (speed * direction), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    break;
                case Axis.Y:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (speed * direction), transform.rotation.eulerAngles.z);
                    break;
                case Axis.Z:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (speed * direction));
                    break;
            }
        }
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}