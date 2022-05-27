using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Loader : MonoBehaviour
    {
        private int loadingAnimation = Animator.StringToHash("Loading");
        private Animator animator;
        private bool animationIsFinished;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            LoadGame();
        }

        private void LoadGame()
        {
            StartCoroutine(LoadSceneAsync());
        }

        private bool CheckInternetConnection()
        {
            return Application.internetReachability == NetworkReachability.NotReachable;
        }

        public void FinishAnimation()
        {
            animationIsFinished = true;
        }
        
        private IEnumerator LoadSceneAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync("Game");
            
            animator.Play(loadingAnimation);
            
            while (!asyncLoad.isDone || !animationIsFinished)
            {
                yield return null;
            }
                
            animator.StopPlayback();
        }
    }
}