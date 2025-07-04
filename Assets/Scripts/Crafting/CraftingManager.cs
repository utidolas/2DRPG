using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;

    [Header("Recipe Info")]
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Image item1Icon;
    [SerializeField] private TextMeshProUGUI item1Name;
    [SerializeField] private TextMeshProUGUI item1Amount;
    [SerializeField] private Image item2Icon;
    [SerializeField] private TextMeshProUGUI item2Name;
    [SerializeField] private TextMeshProUGUI item2Amount;
    [SerializeField] private Button craftButton;

    [Header("Final Item")]
    [SerializeField] private Image finalItemIcon;
    [SerializeField] private TextMeshProUGUI finalItemName;
    [SerializeField] private TextMeshProUGUI finalItemDescription;

    [Header("Recipes")]
    [SerializeField] private RecipeList recipes;

    private void Start()
    {
        // Load the recipes when the game starts
        LoadRecipes();
    }

    private void LoadRecipes()
    {
        // iterate through the recipes and instantiate a RecipeCard for each one
        for (int i = 0; i < recipes.Recipes.Length; i++)
        {
            RecipeCard card = Instantiate(recipeCardPrefab, recipeContainer);
            card.InitRecipeCard(recipes.Recipes[i]);
        }
    }

    // update the recipe info when a recipe card is clicked
    public void ShowRecipe(Recipe recipe)
    {
        recipeName.text = recipe.Name;

        item1Icon.sprite = recipe.Ingredient1.Icon;
        item1Name.text = recipe.Ingredient1.Name;

        item2Icon.sprite = recipe.Ingredient2.Icon;
        item2Name.text = recipe.Ingredient2.Name;

        item1Amount.text = $"{Inventory.Instance.GetItemStock(recipe.Ingredient1.ID)}/{recipe.Ingredient1Amount}";
        item2Amount.text = $"{Inventory.Instance.GetItemStock(recipe.Ingredient2.ID)}/{recipe.Ingredient2Amount}";

        finalItemIcon.sprite = recipe.FinalItem.Icon;
        finalItemName.text = recipe.FinalItem.Name;
        finalItemDescription.text = recipe.FinalItem.Description;

        craftButton.interactable = CanCraftItem(recipe);
    }

    private bool CanCraftItem(Recipe recipe)
    {
        // get stock of ingredients from the inventory
        int item1Stock = Inventory.Instance.GetItemStock(recipe.Ingredient1.ID);
        int item2Stock = Inventory.Instance.GetItemStock(recipe.Ingredient2.ID);

        if (item1Stock >= recipe.Ingredient1Amount && item2Stock >= recipe.Ingredient2Amount)
        {
            return true; // can craft
        }

        return false; // cannot craft
    }

}
