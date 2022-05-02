using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MEC;
using UnityEngine;

namespace Core
{
    public class CameraController : MonoBehaviour
    {
        private Camera camera;
        private float startTime;
        private float journeyLength;
        private float fieldOfView;
        private float endValue;
        private bool isMoving;

        private float speed = 30;
        
        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        public void ChangeFieldView(float speedValue)
        {
            switch (speedValue)
            {
                case 3:
                    DoFieldOfView(60);
                    break;
                case 4:
                    DoFieldOfView(65);
                    break;
                case 5:
                    DoFieldOfView(70);
                    break;
                case 6:
                    DoFieldOfView(75);
                    break;
                case 7:
                    DoFieldOfView(78);
                    break;
                case 8:
                    DoFieldOfView(80);
                    break;
                default:
                    DoFieldOfView(60);
                    break;
            }
        }

        private void DoFieldOfView(float endValue)
        {
            isMoving = true;
            startTime = Time.time;
            this.endValue = endValue;
            fieldOfView = camera.fieldOfView;
            journeyLength = Mathf.Abs(endValue - fieldOfView);
        }
        
        private void Update()
        {
            if (journeyLength == 0) return;
            if (!isMoving) return;
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            camera.fieldOfView = Mathf.Lerp(fieldOfView, endValue, fractionOfJourney);
            if (fractionOfJourney >= 1) isMoving = false;
        }
        
        /*private Camera camera;
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
                    //camera.DOFieldOfView(60, 0.35f);
                    AnimateFieldOfView(60);
                    break;
                case 4:
                    //camera.DOFieldOfView(65, 0.35f);
                    AnimateFieldOfView(65);
                    break;
                case 5:
                    //camera.DOFieldOfView(70, 0.35f);
                    AnimateFieldOfView(70);
                    break;
                case 6:
                    //camera.DOFieldOfView(75, 0.35f);
                    AnimateFieldOfView(75);
                    break;
                case 7:
                    //camera.DOFieldOfView(78, 0.35f);
                    AnimateFieldOfView(78);
                    break;
                case 8:
                    //camera.DOFieldOfView(80, 0.35f);
                    AnimateFieldOfView(80);
                    break;
            }
        }

        private async UniTask AnimateFieldOfView(float targetFieldOfView)
        {
            journeyLength = Mathf.Abs(targetFieldOfView - camera.fieldOfView);
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            var currentFieldOfView = camera.fieldOfView;
            
            while (fractionOfJourney < 1)
            {
                distCovered = (Time.time - startTime) * speed;
                fractionOfJourney = distCovered / journeyLength;
                camera.fieldOfView = Mathf.Lerp(currentFieldOfView, targetFieldOfView, fractionOfJourney);
                await UniTask.Yield();
            }
        }*/
        
        
        /*private Camera camera;
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
                    //camera.DOFieldOfView(60, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(60));
                    break;
                case 4:
                    //camera.DOFieldOfView(65, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(65));
                    break;
                case 5:
                    //camera.DOFieldOfView(70, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(70));
                    break;
                case 6:
                    //camera.DOFieldOfView(75, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(75));
                    break;
                case 7:
                    //camera.DOFieldOfView(78, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(78));
                    break;
                case 8:
                    //camera.DOFieldOfView(80, 0.35f);
                    if (fieldOfViewCoroutine != null) StopCoroutine(fieldOfViewCoroutine);
                    fieldOfViewCoroutine = StartCoroutine(AnimateFieldOfView(80));
                    break;
            }
        }

        private IEnumerator AnimateFieldOfView(float targetFieldOfView)
        {
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            var currentFieldOfView = camera.fieldOfView;
            
            while (fractionOfJourney < 1)
            {
                distCovered = (Time.time - startTime) * speed;
                fractionOfJourney = distCovered / journeyLength;
                camera.fieldOfView = Mathf.Lerp(currentFieldOfView, targetFieldOfView, fractionOfJourney);
                yield return null;
            }
        }*/
    }
}