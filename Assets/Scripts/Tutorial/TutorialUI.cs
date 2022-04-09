using System.IO;
using Data.Progress;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TutorialProgress tutorialProgress;

    private string path;
    
    private void Awake()
    {
        tutorialProgress = new TutorialProgress();
        tutorialProgress.tutorialSteps = new TutorialStep[3];
        
        for (var i = 0; i < tutorialProgress.tutorialSteps.Length; i++)
        {
            tutorialProgress.tutorialSteps[i].innerSteps = new bool[3];
        }
        
        var json= JsonUtility.ToJson(tutorialProgress);
        path = Path.Combine(Application.dataPath, "tutorialProgress.json");
        if (!File.Exists(path)) File.WriteAllText(path, json);
        tutorialProgress = JsonUtility.FromJson<TutorialProgress>(File.ReadAllText(path));
    }
}
