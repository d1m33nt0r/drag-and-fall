using System.IO;
using Core;
using Data.Progress;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TutorialProgress tutorialProgress;
    [SerializeField] private GameManager gameManager;
    private string path;
    [SerializeField] private GameObject firstStep;
    [SerializeField] private GameObject secondStep;
    [SerializeField] private GameObject thirdStep;
    [SerializeField] private GameObject fourthStep;
    
    public bool tutorialIsFinished => firstStepComplete && secondStepComplete && thirdStepComplete;

    public bool firstGeneralStepComplete => fourthStepComplete;
    public bool firstStepComplete;
    public bool secondStepComplete;
    public bool thirdStepComplete;
    public bool fourthStepComplete;
    
    private int currentStep;

    private void Awake()
    {
        tutorialProgress = new TutorialProgress();
        tutorialProgress.tutorialSteps = new bool[4];
        
        var json= JsonUtility.ToJson(tutorialProgress);
#if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, "tutorialProgress.json");
#elif UNITY_ANDROID
        path = Path.Combine(Application.persistentDataPath, "tutorialProgress.json");
#endif
        if (!File.Exists(path)) File.WriteAllText(path, json);
        tutorialProgress = JsonUtility.FromJson<TutorialProgress>(File.ReadAllText(path));
        firstStepComplete = tutorialProgress.tutorialSteps[0];
        secondStepComplete = tutorialProgress.tutorialSteps[1];
        thirdStepComplete = tutorialProgress.tutorialSteps[2];
        fourthStepComplete = tutorialProgress.tutorialSteps[3];
        
        if (!firstStepComplete)
        {
            currentStep = 0;
            return;
        }

        if (!secondStepComplete)
        {
            currentStep = 1;
            return;
        }

        if (!thirdStepComplete)
        {
            currentStep = 2;
            return;
        }
        if (!fourthStepComplete)
        {
            currentStep = 3;
        }
    }

    public void ShowFirstStep()
    {
        firstStep.SetActive(true);
        secondStep.SetActive(false);
        thirdStep.SetActive(false);
        fourthStep.SetActive(false);
    }

    public void ShowSecondStep()
    {
        gameObject.SetActive(true);
        firstStep.SetActive(false);
        secondStep.SetActive(true);
        fourthStep.SetActive(false);
        thirdStep.SetActive(false);
        firstStepComplete = true;
        secondStepComplete = true;
        gameManager.gameStarted = false;
    }

    public void ShowThirdStep()
    {
        gameObject.SetActive(true);
        firstStep.SetActive(false);
        secondStep.SetActive(false);
        thirdStep.SetActive(true);
        fourthStep.SetActive(false);
        thirdStepComplete = true;
        gameManager.gameStarted = false;
    }

    public void ShowFourthStep()
    {
        gameObject.SetActive(true);
        firstStep.SetActive(false);
        secondStep.SetActive(false);
        thirdStep.SetActive(false);
        fourthStep.SetActive(true);
        firstStepComplete = true;
        secondStepComplete = true;
        thirdStepComplete = true;
        fourthStepComplete = true;
        tutorialProgress.tutorialSteps[0] = true;
        tutorialProgress.tutorialSteps[1] = true;
        tutorialProgress.tutorialSteps[2] = true;
        tutorialProgress.tutorialSteps[3] = true;
        RewriteTutorialProgressData();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Finished 1 stage of tutorial");
    }

    public void RewriteTutorialProgressData()
    {
        var json= JsonUtility.ToJson(tutorialProgress);
#if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, "tutorialProgress.json");
#elif UNITY_ANDROID
        path = Path.Combine(Application.persistentDataPath, "tutorialProgress.json");
#endif
        File.WriteAllText(path, json);
    }
}
