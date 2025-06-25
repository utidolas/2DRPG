using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("Config")]
    [SerializeField] private ShopCard shopCardPrefab;
    [SerializeField] private Transform shopCardContainer;

    // array of items to load into the shop
    [Header("Items")]
    [SerializeField] private ShopItem[] shopItems;

    private void Start()
    {
        LoadShop();
    }

    private void LoadShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopCard card = Instantiate(shopCardPrefab, shopCardContainer);
            card.ConfigShopCard(shopItems[i]);
        }
    }

}
