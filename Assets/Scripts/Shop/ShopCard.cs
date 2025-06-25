using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI buyAmount;

    public ShopItem item { get; set; } // item to load into the card
    private int quantity;
    private float initialCost; // initial cost of the item before add or subtracting amount
    private float currentCost;

    private void Update()
    {
        // Update the UI with the current quantity and cost
        buyAmount.text = quantity.ToString();
        itemCost.text = currentCost.ToString();
    }

    public void ConfigShopCard(ShopItem shopItem)
    {
        item = shopItem;
        itemIcon.sprite = shopItem.Item.Icon;
        itemName.text = shopItem.Item.Name;
        itemCost.text = shopItem.Cost.ToString();
        quantity = 1; // default quantity to buy
        initialCost = shopItem.Cost;
        currentCost = shopItem.Cost;
    }

    public void Add()
    {
        float buyCost = initialCost * (quantity + 1);

        // Check if the player has enough coins to buy the next amount, then increase the amount to buy and update the cost
        if (CoinManager.Instance.Coins >= buyCost)
        {
            quantity++;
            currentCost = initialCost * quantity;
        }
    }

    public void Remove()
    {
        // Ensure that the amount to buy does not go below 1
        if (quantity == 1) return;
        quantity--;
        currentCost = initialCost * quantity;
    }
}
