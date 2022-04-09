using System;

namespace Data.Progress
{
    [Serializable]
    public class TutorialProgress
    {
        public TutorialStep[] tutorialSteps;
    }

    [Serializable]
    public struct TutorialStep
    {
        public bool[] innerSteps;
    }
}