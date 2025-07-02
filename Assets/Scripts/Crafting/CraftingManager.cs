using UnityEngine;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;

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
}
