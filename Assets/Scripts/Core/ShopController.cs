using System;
using Data;
using Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Text unlockedText;
        [SerializeField] private GameObject coinPricePanel;
        [SerializeField] private Text priceCoins;
        [SerializeField] private GameObject crystalPricePanel;
        [SerializeField] private Text priceCrystals;
        [SerializeField] private GameObject price;
        
        [SerializeField] private Player player;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        [SerializeField] private Button unlockButton;
        [SerializeField] private Button selectButton;
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

        public void SetDefaultState()
        {
            SetPriceText(progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought, 
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins,
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
            shopState = ShopState.EnvironmentSkin;
        }
        
        private void SetPriceText(bool _isBought, float _priceCoins, float priceCrystals)
        {
            if (!_isBought)
            {
                if (priceCrystals != 0)
                {
                    this.priceCrystals.gameObject.SetActive(true);
                    this.priceCrystals.text = priceCrystals.ToString();
                    this.crystalPricePanel.SetActive(true);
                }
                else
                {
                    this.priceCrystals.gameObject.SetActive(false);
                    this.crystalPricePanel.SetActive(false);
                }
                coinPricePanel.SetActive(true);
                price.gameObject.SetActive(true);
                priceCoins.text = _priceCoins.ToString();
                unlockedText.gameObject.SetActive(false);
                priceCoins.gameObject.SetActive(true);
                unlockButton.gameObject.SetActive(true);
                selectButton.gameObject.SetActive(false);
            }
            else
            {
                //price.text = "Unlocked!";
                coinPricePanel.SetActive(false);
                crystalPricePanel.SetActive(false);
                price.SetActive(false);
                unlockedText.gameObject.SetActive(true);
                this.priceCrystals.gameObject.SetActive(false);
                priceCoins.gameObject.SetActive(false);
                unlockButton.gameObject.SetActive(false);
                selectButton.gameObject.SetActive(true);
            }
        }

        public void BuyItem()
        {
            switch (shopState)
            {
                case ShopState.EnvironmentSkin:
                    if (shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins > progressController.currentState.currenciesProgress.coins) return;
                    if (shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals > progressController.currentState.currenciesProgress.crystals) return;
                    
                    progressController.currentState.currenciesProgress.coins -=
                        Convert.ToInt16(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins);
                    progressController.currentState.currenciesProgress.crystals -=
                        Convert.ToInt16(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
                    
                    progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought = true;
                    progressController.currentState.environmentSkin = new ShopItem
                    {
                        index = currentEnvironmentSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].isBought = true;
                    SetPriceText(true, shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
                    coinPanel.MinusCoins(Convert.ToInt32(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins));
                    crystalPanel.MinusCrystals(Convert.ToInt32(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals));
                    break;
                case ShopState.PlayerSkin:
                    if (shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins > progressController.currentState.currenciesProgress.coins) return;
                    if (shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals > progressController.currentState.currenciesProgress.crystals) return;
                    
                    progressController.currentState.currenciesProgress.coins -=
                        Convert.ToInt16(shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins);
                    progressController.currentState.currenciesProgress.crystals -=
                        Convert.ToInt16(shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    
                    progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought = true;
                    progressController.currentState.playerSkin = new ShopItem
                    {
                        index = currentPlayerSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    shopData.PlayerSkinData[currentPlayerSkinIndex].isBought = true;
                    SetPriceText(true, shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    coinPanel.MinusCoins(Convert.ToInt32(shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins));
                    crystalPanel.MinusCrystals(Convert.ToInt32(shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals));
                    break;
                case ShopState.TrailSkin:
                    if (shopData.TrailSkinData[currentTrailSkinIndex].priceCoins > progressController.currentState.currenciesProgress.coins) return;
                    if (shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals > progressController.currentState.currenciesProgress.crystals) return;
                    
                    progressController.currentState.currenciesProgress.coins -=
                        Convert.ToInt16(shopData.TrailSkinData[currentTrailSkinIndex].priceCoins);
                    progressController.currentState.currenciesProgress.crystals -=
                        Convert.ToInt16(shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    
                    progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought = true;
                    progressController.currentState.trailSkin = new ShopItem
                    {
                        index = currentTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    shopData.TrailSkinData[currentTrailSkinIndex].isBought = true;
                    SetPriceText(true, shopData.TrailSkinData[currentTrailSkinIndex].priceCoins, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    coinPanel.MinusCoins(Convert.ToInt32(shopData.TrailSkinData[currentTrailSkinIndex].priceCoins));
                    crystalPanel.MinusCrystals(Convert.ToInt32(shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals));
                    break;
                case ShopState.FallingTrailSkin:
                    if (shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins > progressController.currentState.currenciesProgress.coins) return;
                    if (shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals > progressController.currentState.currenciesProgress.crystals) return;
                    
                    progressController.currentState.currenciesProgress.coins -=
                        Convert.ToInt16(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins);
                    progressController.currentState.currenciesProgress.crystals -=
                        Convert.ToInt16(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals);
                    
                    progressController.shopProgress.fallingTrailSkins[currentFallingTrailSkinIndex].isBought = true;
                    progressController.currentState.fallingTrailSkin = new ShopItem
                    {
                        index = currentFallingTrailSkinIndex, 
                        isBought = true
                    };
                    progressController.SaveShopData(progressController.shopProgress);
                    progressController.SaveCurrentState(progressController.currentState);
                    shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].isBought = true;
                    SetPriceText(true, shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals);
                    coinPanel.MinusCoins(Convert.ToInt32(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins));
                    crystalPanel.MinusCrystals(Convert.ToInt32(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals));
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

        public void SetFallingTrailSkinState()
        {
            shopState = ShopState.FallingTrailSkin;
            SetPriceText(progressController.shopProgress.fallingTrailSkins[currentFallingTrailSkinIndex].isBought, 
                shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins, 
                shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals);
            gameManager.ChangeShopState(shopState);
        }

        public void SetPlayerSkinState()
        {
            shopState = ShopState.PlayerSkin;
            SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins,
                shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
            gameManager.ChangeShopState(shopState);
        }

        public void SetEnvironmentSkinState()
        {
            shopState = ShopState.EnvironmentSkin;
            SetPriceText(progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought, 
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins,
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
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
                    
                    SetPriceText(progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
                    platformMover.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == shopData.PlayerSkinData.Count - 1)
                        currentPlayerSkinIndex = 0;
                    else
                        currentPlayerSkinIndex++;
                    
                    SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins,
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == shopData.TrailSkinData.Count - 1)
                        currentTrailSkinIndex = 0;
                    else
                        currentTrailSkinIndex++;
                    
                    SetPriceText(progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCoins,
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == shopData.FallingTrailSkinData.Count - 1)
                        currentFallingTrailSkinIndex = 0;
                    else
                        currentFallingTrailSkinIndex++;

                    SetPriceText(progressController.shopProgress.fallingTrailSkins[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals);
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
                    
                    SetPriceText(progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins, 
                        shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
                    platformMover.TryOnSkin(shopData.EnvironmentSkinData[currentEnvironmentSkinIndex]);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == 0)
                        currentPlayerSkinIndex = shopData.PlayerSkinData.Count - 1;
                    else
                        currentPlayerSkinIndex--;
                    
                    SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    player.TryOnPlayerSkin(shopData.PlayerSkinData[currentPlayerSkinIndex].mesh, shopData.PlayerSkinData[currentPlayerSkinIndex].material);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == 0)
                        currentTrailSkinIndex = shopData.TrailSkinData.Count - 1;
                    else
                        currentTrailSkinIndex--;
                    
                    SetPriceText(progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCoins, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    player.TryOnTrailSkin(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
                case ShopState.FallingTrailSkin:
                    if (currentFallingTrailSkinIndex == 0)
                        currentFallingTrailSkinIndex = shopData.FallingTrailSkinData.Count - 1;
                    else
                        currentFallingTrailSkinIndex--;
                    
                    SetPriceText(progressController.shopProgress.fallingTrailSkins[currentFallingTrailSkinIndex].isBought, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCoins, 
                        shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].priceCrystals);
                    player.TryOnFallingTrailSkin(shopData.FallingTrailSkinData[currentFallingTrailSkinIndex].skin);
                    break;
            }
        }
    }
}