using UnityEngine;

[CreateAssetMenu(fileName = "RecipeList", menuName = "Crafting/Recipe List", order = 1)]
public class RecipeList : ScriptableObject
{
    public Recipe[] Recipes;
}
