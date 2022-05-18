using Core;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class TutorialHandButton : Button
{
    [Inject] private GameManager gameManager;
    
    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        gameManager.StartGame();
    }
}
