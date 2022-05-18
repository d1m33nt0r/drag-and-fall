using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] private RectTransform playerSkinsBtn;
        [SerializeField] private RectTransform trailSkinsBtn;
        [SerializeField] private RectTransform themeSkinsBtn;

        public void ActivePlayerSkins()
        {
            playerSkinsBtn.offsetMax = new Vector2(playerSkinsBtn.offsetMax.x, 22);
            trailSkinsBtn.offsetMax = new Vector2(trailSkinsBtn.offsetMax.x, 0);
            themeSkinsBtn.offsetMax = new Vector2(themeSkinsBtn.offsetMax.x, 0);
            playerSkinsBtn.GetComponent<Image>().color = new Color(0.76f, 0.76f, 0.76f, 0.5f);
            trailSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
            themeSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
        }

        public void ActiveTrailSkins()
        {
            playerSkinsBtn.offsetMax = new Vector2(playerSkinsBtn.offsetMax.x, 0);
            trailSkinsBtn.offsetMax = new Vector2(trailSkinsBtn.offsetMax.x, 22);
            themeSkinsBtn.offsetMax = new Vector2(themeSkinsBtn.offsetMax.x, 0);
            trailSkinsBtn.GetComponent<Image>().color = new Color(0.76f, 0.76f, 0.76f, 0.5f);
            playerSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
            themeSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
        }

        public void ActiveThemeSkins()
        {
            playerSkinsBtn.offsetMax = new Vector2(playerSkinsBtn.offsetMax.x, 0);
            trailSkinsBtn.offsetMax = new Vector2(trailSkinsBtn.offsetMax.x, 0);
            themeSkinsBtn.offsetMax = new Vector2(themeSkinsBtn.offsetMax.x, 22);
            themeSkinsBtn.GetComponent<Image>().color = new Color(0.76f, 0.76f, 0.76f, 0.5f);
            trailSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
            playerSkinsBtn.GetComponent<Image>().color = new Color(0.54f, 0.54f, 0.54f, 0.5f);
        }
    }
}