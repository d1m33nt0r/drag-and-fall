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
    
    public bool tutorialIsFinished => firstStepComplete && secondStepComplete && thirdStepComplete;
    
    public bool firstStepComplete;
    public bool secondStepComplete;
    public bool thirdStepComplete;

    private int currentStep;

    private void Awake()
    {
        tutorialProgress = new TutorialProgress();
        tutorialProgress.tutorialSteps = new bool[3];
        
        var json= JsonUtility.ToJson(tutorialProgress);
        path = Path.Combine(Application.dataPath, "tutorialProgress.json");
        if (!File.Exists(path)) File.WriteAllText(path, json);
        tutorialProgress = JsonUtility.FromJson<TutorialProgress>(File.ReadAllText(path));
        firstStepComplete = tutorialProgress.tutorialSteps[0];
        secondStepComplete = tutorialProgress.tutorialSteps[1];
        thirdStepComplete = tutorialProgress.tutorialSteps[2];

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
        }
    }

    public void ShowFirstStep()
    {
        firstStep.SetActive(true);
        secondStep.SetActive(false);
        thirdStep.SetActive(false);
        firstStepComplete = true;
    }

    public void ShowSecondStep()
    {
        gameObject.SetActive(true);
        firstStep.SetActive(false);
        secondStep.SetActive(true);
        thirdStep.SetActive(false);
        secondStepComplete = true;
        gameManager.gameStarted = false;
    }

    public void ShowThirdStep()
    {
        firstStep.SetActive(false);
        secondStep.SetActive(false);
        thirdStep.SetActive(true);
        thirdStepComplete = true;
        gameManager.gameStarted = false;
    }
}
