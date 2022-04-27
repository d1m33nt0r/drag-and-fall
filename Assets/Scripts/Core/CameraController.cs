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
        
        private float speed = 80;
        
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
            if (!isMoving) return;
            var distCovered = (Time.time - startTime) * speed;
            var fractionOfJourney = distCovered / journeyLength;
            camera.fieldOfView = Mathf.Lerp(fieldOfView, endValue, fractionOfJourney);
            if (fractionOfJourney >= 1) isMoving = false;
        }
    }
}