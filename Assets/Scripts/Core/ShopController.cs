using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text shopStateText;
        
        public ShopState shopState;

        public void MoveLeftState()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    shopState = ShopState.PlayerSkin;
                    shopStateText.text = "Player skins";
                    break;
                case ShopState.PlayerSkin:
                    shopState = ShopState.TrailSkin;
                    shopStateText.text = "Trail skins";
                    break;
                case ShopState.TrailSkin:
                    shopState = ShopState.FallingTrailSkin;
                    shopStateText.text = "Falling trail skins";
                    break;
                case ShopState.FallingTrailSkin:
                    shopState = ShopState.EnvironmentSkin;
                    shopStateText.text = "Environment skins";
                    break;
            }
            
            gameManager.ChangeShopState(shopState);
        }

        public void MoveRightState()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    shopState = ShopState.FallingTrailSkin;
                    shopStateText.text = "Falling trail skins";
                    break;
                case ShopState.PlayerSkin:
                    shopState = ShopState.EnvironmentSkin;
                    shopStateText.text = "Environment skins";
                    break;
                case ShopState.TrailSkin:
                    shopState = ShopState.PlayerSkin;
                    shopStateText.text = "Player skins";
                    break;
                case ShopState.FallingTrailSkin:
                    shopState = ShopState.TrailSkin;
                    shopStateText.text = "Trail skins";
                    break;
            }
            
            gameManager.ChangeShopState(shopState);
        }
    }
}