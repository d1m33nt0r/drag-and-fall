using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
        private Camera camera;
        private float startTime;
        private float journeyLength;

        private Coroutine fieldOfViewCoroutine;
        
        [SerializeField] private float speed;
        
        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        public void ChangeFieldView(float speedValue)
        {
            startTime = Time.time;
            switch (speedValue)
            {
                case 3:
                    camera.DOFieldOfView(60, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(60));
                    break;
                case 4:
                    camera.DOFieldOfView(65, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(65));
                    break;
                case 5:
                    camera.DOFieldOfView(70, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(70));
                    break;
                case 6:
                    camera.DOFieldOfView(75, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(75));
                    break;
                case 7:
                    camera.DOFieldOfView(78, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(75));
                    break;
                case 8:
                    camera.DOFieldOfView(80, 0.35f);
                    //if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    //fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(75));
                    break;
            }
        }
    }
}