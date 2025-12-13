using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
public class DishManager : MonoBehaviour
{
    public static DishManager Instance;
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
    private void Start()
    {
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
        // GameObject matchedRecipe = null;
        // foreach (var recipe in configDishTable) 
        // {
        //     var allMatch = true;
        //     // Check each ingredient required by this recipe
        //     foreach (var required in recipe.Ingredients)
        //     {
        //         var found = ingredients.Any(item =>
        //         {
        //             var ing = item.GetComponent<Ingredient>();
        //             return ing && 
        //                    ing.PrepType == required.PrepType && 
        //                    ing.Name == required.PoolType && 
        //                    ing.ProcessType == required.ProcessType;
        //         });
        //         if (found) continue;
        //         allMatch = false;
        //         break;
        //     }
        //     // Extra check: make sure you don’t have more ingredients than the recipe
        //     if (!allMatch || ingredients.Count != recipe.Ingredients.Count) continue;
        //     matchedRecipe = recipe.Output;
        //     break;
        // }
        var ingNames = new List<string>();
        var preps = new List<string>();
        var procs = new List<string>();
        foreach (var ingObj in ingredients)
        {
            var ingredient = ingObj.GetComponent<Ingredient>();
            ingNames.Add(ingredient.Name);
            preps.Add(ingredient.PrepType.ToString());
            procs.Add(ingredient.ProcessType.ToString());
        }
        if (!ConfigManager.Instance) return null;
        var mathRecipe = ConfigManager.Instance.ConfigDishes.GetConfigByIngPrepProc(ingNames.ToArray(), preps.ToArray(), procs.ToArray());
        if (mathRecipe == null) return null;
        return Resources.Load<GameObject>("Prefabs/Dishes/"+mathRecipe.prefab);
    }

}