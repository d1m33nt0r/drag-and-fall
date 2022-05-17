using System;
using Data;
using Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class ShopController : MonoBehaviour
    {
        [Inject] private SoundOfBuying soundOfBuying;
        
        [SerializeField] private TextMeshProUGUI unlockedText;
        [SerializeField] private GameObject coinPricePanel;
        [SerializeField] private TextMeshProUGUI priceCoins;
        [SerializeField] private GameObject crystalPricePanel;
        [SerializeField] private TextMeshProUGUI priceCrystals;
        [SerializeField] private GameObject price;
        
        [SerializeField] private Player player;
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private CoinPanel coinPanel;
        [SerializeField] private CrystalPanel crystalPanel;
        [SerializeField] private Button unlockButton;
        [SerializeField] private Button selectButton;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ShopData shopData;
        [SerializeField] private ProgressController progressController;

        private int currentPlayerSkinIndex;
        private int currentEnvironmentSkinIndex;
        private int currentTrailSkinIndex;

        public ShopState shopState;

        private void Start()
        {
            ResetCurrent();
        }

        public void ResetCurrent()
        {
            currentEnvironmentSkinIndex = progressController.currentState.environmentSkin.index;
            currentPlayerSkinIndex = progressController.currentState.playerSkin.index;
            currentTrailSkinIndex = progressController.currentState.trailSkin.index;
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
                    soundOfBuying.PlayBuyingSound();
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
                    soundOfBuying.PlayBuyingSound();
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
                    soundOfBuying.PlayBuyingSound();
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
            }
        }

        public void SetPlayerSkinState()
        {
            shopState = ShopState.PlayerSkin;
            SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins,
                shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
            /*var themeIdentifier3 = progressController.currentState.environmentSkin.index.ToString() + progressController.currentState.environmentSkin.index;
            platformMover.ChangeTheme(themeIdentifier3, progressController.currentState.environmentSkin.index);
            player.ChangeTheme(themeIdentifier3);*/
            gameManager.ChangeShopState(shopState);
        }

        public void SetTrailSkinState()
        {
            shopState = ShopState.TrailSkin;
            SetPriceText(progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought, 
                shopData.TrailSkinData[currentTrailSkinIndex].priceCoins,
                shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
            /*var themeIdentifier3 = progressController.currentState.environmentSkin.index.ToString() + progressController.currentState.playerSkin.index;
            platformMover.ChangeTheme(themeIdentifier3, progressController.currentState.environmentSkin.index);
            player.ChangeTheme(themeIdentifier3);*/
            player.TryOnTrail(shopData.TrailSkinData[currentTrailSkinIndex].skin);
            gameManager.ChangeShopState(shopState);
        }
        
        public void SetEnvironmentSkinState()
        {
            shopState = ShopState.EnvironmentSkin;
            SetPriceText(progressController.shopProgress.environmentSkins[currentEnvironmentSkinIndex].isBought, 
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCoins,
                shopData.EnvironmentSkinData[currentEnvironmentSkinIndex].priceCrystals);
            
            /*var themeIdentifier = progressController.currentState.environmentSkin.index.ToString() + progressController.currentState.playerSkin.index;
            platformMover.ChangeTheme(themeIdentifier, progressController.currentState.environmentSkin.index);
            player.ChangeTheme(themeIdentifier);*/
            
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
                    var themeIdentifier = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == shopData.PlayerSkinData.Count - 1)
                        currentPlayerSkinIndex = 0;
                    else
                        currentPlayerSkinIndex++;
                    
                    SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins,
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    var themeIdentifier2 = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier2, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier2);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == shopData.TrailSkinData.Count - 1)
                        currentTrailSkinIndex = 0;
                    else
                        currentTrailSkinIndex++;
                    
                    var themeIdentifier3 = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier3, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier3);
                    
                    SetPriceText(progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCoins,
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    player.TryOnTrail(shopData.TrailSkinData[currentTrailSkinIndex].skin);
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
                    var themeIdentifier = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier);
                    break;
                case ShopState.PlayerSkin:
                    if (currentPlayerSkinIndex == 0)
                        currentPlayerSkinIndex = shopData.PlayerSkinData.Count - 1;
                    else
                        currentPlayerSkinIndex--;
                    
                    SetPriceText(progressController.shopProgress.playerSkins[currentPlayerSkinIndex].isBought, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCoins, 
                        shopData.PlayerSkinData[currentPlayerSkinIndex].priceCrystals);
                    var themeIdentifier2 = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier2, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier2);
                    break;
                case ShopState.TrailSkin:
                    if (currentTrailSkinIndex == 0)
                        currentTrailSkinIndex = shopData.TrailSkinData.Count - 1;
                    else
                        currentTrailSkinIndex--;
                    
                    var themeIdentifier3 = currentEnvironmentSkinIndex.ToString() + currentPlayerSkinIndex;
                    platformMover.ChangeTheme(themeIdentifier3, currentEnvironmentSkinIndex);
                    player.ChangeTheme(themeIdentifier3);
                    
                    SetPriceText(progressController.shopProgress.trailSkins[currentTrailSkinIndex].isBought, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCoins, 
                        shopData.TrailSkinData[currentTrailSkinIndex].priceCrystals);
                    player.TryOnTrail(shopData.TrailSkinData[currentTrailSkinIndex].skin);
                    break;
            }
        }
    }
}