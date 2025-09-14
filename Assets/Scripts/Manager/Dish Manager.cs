using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
    private void OnEnable() {
        EventManager.Instance.onFindRecipeOutput += FindRecipe;
    }
    private void OnDisable()
    {
        EventManager.Instance.onFindRecipeOutput -= FindRecipe;
    }
    private GameObject FindRecipe(List<GameObject> ingredients)
    {
        foreach (RecipeSO recipe in recipeSOs)
        {
            if (recipe.Ingredients.Count != ingredients.Count) continue;
            int matchCount = 0;
            foreach (var item in ingredients)
            {
                Ingredient ingredient = item.GetComponent<Ingredient>();
                if (ingredient && recipe.Ingredients.Find(x => x.PrepType == ingredient.PrepType) &&
                    recipe.Ingredients.Find(x => x.PoolType == ingredient.PoolType))
                {
                    matchCount++;
                }
            }
            if(matchCount != ingredients.Count) continue;
            return recipe.Output;
        }
        return null;
    }
}