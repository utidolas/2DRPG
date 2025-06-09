using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;

    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(Index);
    }

    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityContainer;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public int Index { get; set; }

    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon;
        itemQuantityTMP.text = item.Quantity.ToString();
    }

    public void ShowSlotInformation(bool value)
    {
        itemIcon.gameObject.SetActive(value);
        quantityContainer.gameObject.SetActive(value);
    }
}
