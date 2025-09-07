using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DishManager : MonoBehaviour
{
    public static DishManager Instance;
    [SerializeField] private List<RecipeSO> recipeSOs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private bool Combie(List<IngredientSO> ingredients)
    {
        int recipeNum = recipeSOs.Where(recipe =>
        {
            var hashIngre = ingredients.ToHashSet<IngredientSO>();
            return hashIngre.IsSubsetOf(recipe.GetIngredientSOs());
        }).Count();
        if (recipeNum > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}