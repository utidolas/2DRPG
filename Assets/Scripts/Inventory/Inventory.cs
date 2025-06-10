using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems; // items to be saved inside inventory

    [Header("Testing")]
    public InventoryItem testItem;

    public int InventorySize => inventorySize; // prop to get inventorySize value
    public InventoryItem[] InventoryItems => inventoryItems;

    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 1);
        }
    }

    // add item to inv
    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;
        List<int> itemIndexes = CheckItemStock(item.ID);
        if(item.IsStackable && itemIndexes.Count > 0) // if we have same item in inv
        {
            foreach(int index in itemIndexes)
            {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack) 
                {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int dif = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, dif);
                    }

                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    return;
                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity; // if quantity IS greater than itemMaxStack, quantityToAdd = item.MaxStack, otherwise quantityToAdd = quantity
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if(remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return; // if we don't have item, return.
        if (inventoryItems[index].UseItem()) // use item
        {
            DecreaseItemStack(index); // decrease item stack
        }
    }

    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);
    }

    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Weapon) return;
        inventoryItems[index].EquipItem();
    }

    // add to free slot
    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    private void DecreaseItemStack(int index)
    {
        inventoryItems[index].Quantity--;
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index); // hide slot info

        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index); // update item
        }
    }

    private List<int> CheckItemStock(string itemID)
    {
        // check if have the same item in inv
        List<int> itemIndexes = new List<int>();
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemID) // found item in inv
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    // clear inventory images
    private void VerifyItemsForDraw()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null) // check all inventory
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }
}
