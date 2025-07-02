using System;
using UnityEngine;

// Store info of each recipe
[Serializable]
public class Recipe
{
    public string Name;
    [Header("Ingredient 1")]
    public InventoryItem Ingredient1;
    public int Ingredient1Amount;

    [Header("Ingredient 2")]
    public InventoryItem Ingredient2;
    public int Ingredient2Amount;

    [Header("Final Item")]
    public InventoryItem FinalItem;
    public int FinalItemAmount;
}
