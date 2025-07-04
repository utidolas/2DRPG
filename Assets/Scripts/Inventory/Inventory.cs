using BayatGames.SaveGameFree;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private GameContent gameContent;
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems; // items to be saved inside inventory

    [Header("Testing")]
    public InventoryItem testItem;

    public int InventorySize => inventorySize; // prop to get inventorySize value
    public InventoryItem[] InventoryItems => inventoryItems;

    private readonly string INVENTORY_KEY_DATA = "MY_INVENTORY";

    public void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();
        LoadInventory();
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
        List<int> itemIndexes = CheckItemStockIndexes(item.ID);
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
                    SaveInventory();
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

        SaveInventory();
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return; // if we don't have item, return.
        if (inventoryItems[index].UseItem()) // use item
        {
            DecreaseItemStack(index); // decrease item stack
        }

        SaveInventory();
    }

    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;
        inventoryItems[index].RemoveItem();
        inventoryItems[index] = null;
        InventoryUI.Instance.DrawItem(null, index);

        SaveInventory();
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

    // public method to decrease item stack when crafting
    public void ConsumeItem(string itemID)
    {
        List<int> indexes = CheckItemStockIndexes(itemID);
        if (indexes.Count > 0) // check if we have at least 1 item
        {
            DecreaseItemStack(indexes[^1]); // consume last item in inv, " ^1 == [indexes.Count  - 1] "
        }
    }

    private List<int> CheckItemStockIndexes(string itemID)
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

    // get currtent stock of item in inventory
    public int GetItemStock(string itemID)
    {
        List<int> indexes = CheckItemStockIndexes(itemID);
        int currentStock = 0;
        // 
        foreach (int index in indexes)
        {
            // if we have item in inventory, add quantity to currentStock
            if (inventoryItems[index].ID == itemID)
            {
                currentStock += inventoryItems[index].Quantity;
            }
        }

        return currentStock;
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

    private InventoryItem ItemExistisInGameContent(string itemID)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (gameContent.GameItems[i].ID == itemID)
            {
                return gameContent.GameItems[i];
            }
        }

        return null;
    }

    private void LoadInventory()
    {
        if (SaveGame.Exists(INVENTORY_KEY_DATA))
        {
            InventoryData loadData = SaveGame.Load<InventoryData>(INVENTORY_KEY_DATA);
            for (int i = 0; i < inventorySize; i++)
            {
                if (loadData.ItemContent[i] != null)
                {
                    InventoryItem itemFromContent = ItemExistisInGameContent(loadData.ItemContent[i]);
                    if(itemFromContent != null)
                    {
                        inventoryItems[i] = itemFromContent.CopyItem(); // create new instance of item in 'i' position
                        inventoryItems[i].Quantity = loadData.ItemQuantity[i];
                        InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                    }
                }
                else
                {
                    inventoryItems[i] = null;
                }
            }
        }
    }

    // Save inventory data logic
    private void SaveInventory()
    {
        InventoryData saveData = new InventoryData();
        saveData.ItemContent = new string[inventorySize];
        saveData.ItemQuantity = new int[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                saveData.ItemContent[i] = null;
                saveData.ItemQuantity[i] = 0;
            }
            else
            {
                saveData.ItemContent[i] = inventoryItems[i].ID;
                saveData.ItemQuantity[i] = inventoryItems[i].Quantity;

            }
        }

        SaveGame.Save(INVENTORY_KEY_DATA, saveData);
    }
}
