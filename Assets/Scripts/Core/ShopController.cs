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

        [SerializeField] private Button unlockButton;
        [SerializeField] private Button selectButton;
        [SerializeField] private Text price;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text shopStateText;
        [SerializeField] private ShopData shopData;
        [SerializeField] private ProgressController progressController;

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
                    SetText(shopData.PlayerSkinData[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].price);
                    shopStateText.text = "Player skins";
                    break;
                case ShopState.PlayerSkin:
                    shopState = ShopState.TrailSkin;
                    SetText(shopData.TrailSkinData[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].price);
                    shopStateText.text = "Trail skins";
                    break;
                case ShopState.TrailSkin:
                    shopState = ShopState.FallingTrailSkin;
                    SetText(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].price);
                    shopStateText.text = "Falling trail skins";
                    break;
                case ShopState.FallingTrailSkin:
                    shopState = ShopState.EnvironmentSkin;
                    SetText(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].price);
                    shopStateText.text = "Environment skins";
                    break;
            }
            
            gameManager.ChangeShopState(shopState);
        }

        private void SetText(bool _isBought, float _price)
        {
            if (!_isBought)
            {
                price.text = _price.ToString();
                unlockButton.gameObject.SetActive(true);
                selectButton.gameObject.SetActive(false);
            }
            else
            {
                price.text = "Unlocked!";
                unlockButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(true);
            }
        }

        public void BuyItem()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought = true;
                    progressController.currentState.environmentSkin = new ShopItem
                    {
                        index = currentEnvironmentSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.PlayerSkin:
                    progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought = true;
                    progressController.currentState.playerSkin = new ShopItem
                    {
                        index = currentPlayerSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.TrailSkin:
                    progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought = true;
                    progressController.currentState.trailSkin = new ShopItem
                    {
                        index = currentTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.FallingTrailSkin:
                    progressController.shopProgress.fallingTrailSkins[currentFallingTrailSkinIndex].isBought = true;
                    progressController.currentState.fallingTrailSkin = new ShopItem
                    {
                        index = currentFallingTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
            }
        }

        public void SelectItem()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    progressController.currentState.environmentSkin = new ShopItem
                    {
                        index = currentEnvironmentSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.PlayerSkin:
                    progressController.currentState.playerSkin = new ShopItem
                    {
                        index = currentPlayerSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.TrailSkin:
                    progressController.currentState.trailSkin = new ShopItem
                    {
                        index = currentTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
                case ShopState.FallingTrailSkin:
                    progressController.currentState.fallingTrailSkin = new ShopItem
                    {
                        index = currentFallingTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveCurrentState(progressController.currentState);
                    break;
            }
        }
        
        public void MoveRightState()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    shopState = ShopState.FallingTrailSkin;
                    SetText(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].price);
                    shopStateText.text = "Falling trail skins";
                    break;
                case ShopState.PlayerSkin:
                    shopState = ShopState.EnvironmentSkin;
                    SetText(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].price);
                    shopStateText.text = "Environment skins";
                    break;
                case ShopState.TrailSkin:
                    shopState = ShopState.PlayerSkin;
                    SetText(shopData.PlayerSkinData[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].price);
                    shopStateText.text = "Player skins";
                    break;
                case ShopState.FallingTrailSkin:
                    shopState = ShopState.TrailSkin;
                    SetText(shopData.TrailSkinData[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].price);
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
                    
                    SetText(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].price);
                    tube.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == shopData.PlayerSkinData.Count - 1)
                        currentPlayerSkinIndex = 0;
                    else
                        currentPlayerSkinIndex++;
                    
                    SetText(shopData.PlayerSkinData[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].price);
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == shopData.TrailSkinData.Count - 1)
                        currentTrailSkinIndex = 0;
                    else
                        currentTrailSkinIndex++;
                    
                    SetText(shopData.TrailSkinData[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].price);
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == shopData.FallingTrailSkinData.Count - 1)
                        currentFallingTrailSkinIndex = 0;
                    else
                        currentFallingTrailSkinIndex++;

                    SetText(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].price);
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
                    
                    SetText(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].price);
                    tube.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == 0)
                        currentPlayerSkinIndex = shopData.PlayerSkinData.Count - 1;
                    else
                        currentPlayerSkinIndex--;
                    
                    SetText(shopData.PlayerSkinData[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].price);
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == 0)
                        currentTrailSkinIndex = shopData.TrailSkinData.Count - 1;
                    else
                        currentTrailSkinIndex--;
                    
                    SetText(shopData.TrailSkinData[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].price);
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == 0)
                        currentFallingTrailSkinIndex = shopData.FallingTrailSkinData.Count - 1;
                    else
                        currentFallingTrailSkinIndex--;
                    
                    SetText(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].price);
                    player.TryOnFallingTrailSkin(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].skin);
                    break;
            }
        }

        public void BuyOrSelectSkin()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    tube.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    player.TryOnFallingTrailSkin(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].skin);
                    break;
            }
        }
    }
}