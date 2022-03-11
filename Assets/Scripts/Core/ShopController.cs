using Data;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Tube tube;
        
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text shopStateText;
        [SerializeField] private ShopData shopData;
        [SerializeField] private ShopProgress shopProgress;

        private int currentPlayerSkinIndex;
        private int currentEnvironmentSkinIndex;
        private int currentTrailSkinIndex;
        private int currentFallingTrailSkinIndex;
        
        public ShopState shopState;

        public void ResetCurrent()
        {
            currentEnvironmentSkinIndex = 0;
            currentPlayerSkinIndex = 0;
            currentTrailSkinIndex = 0;
            currentFallingTrailSkinIndex = 0;
        }
        
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

        public void OnClickNextSkinButton()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    if (currentEnvironmentSkinIndex == shopData.EnvironmentSkinData.Count - 1)
                        currentEnvironmentSkinIndex = 0;
                    else
                        currentEnvironmentSkinIndex++;

                    tube.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == shopData.PlayerSkinData.Count - 1)
                        currentPlayerSkinIndex = 0;
                    else
                        currentPlayerSkinIndex++;
                    
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == shopData.TrailSkinData.Count - 1)
                        currentTrailSkinIndex = 0;
                    else
                        currentTrailSkinIndex++;
                    
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == shopData.FallingTrailSkinData.Count - 1)
                        currentFallingTrailSkinIndex = 0;
                    else
                        currentFallingTrailSkinIndex++;
                    
                    player.TryOnFallingTrailSkin(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].skin);
                    break;
            }
        }

        public void OnClickPrevSkinButton()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    if (currentEnvironmentSkinIndex == 0)
                        currentEnvironmentSkinIndex = shopData.EnvironmentSkinData.Count - 1;
                    else
                        currentEnvironmentSkinIndex--;
                    
                    
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == 0)
                        currentPlayerSkinIndex = shopData.PlayerSkinData.Count - 1;
                    else
                        currentPlayerSkinIndex--;
                    
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == 0)
                        currentTrailSkinIndex = shopData.TrailSkinData.Count - 1;
                    else
                        currentTrailSkinIndex--;
                    
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == 0)
                        currentFallingTrailSkinIndex = shopData.FallingTrailSkinData.Count - 1;
                    else
                        currentFallingTrailSkinIndex--;
                    
                    player.TryOnFallingTrailSkin(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].skin);
                    break;
            }
        }
    }
}