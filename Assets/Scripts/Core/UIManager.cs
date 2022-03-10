using UnityEngine;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas mainMenu;
        [SerializeField] private Canvas game;
        [SerializeField] private Concentration concentration;

        public Concentration Concentration => concentration;
        
        public void SetActiveMainMenu(bool _value) => mainMenu.enabled = _value;
        public void SetActiveGameMenu(bool _value) => game.enabled = _value;
    }
}