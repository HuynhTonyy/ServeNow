using System.Collections.Generic;
using UnityEngine;

public class Container : ItemHolder
{
    private List<GameObject> ingredients;
    public List<GameObject> Ingredients { get { return ingredients; } }
    private GameObject ingredientObjects;
    private void Start()
    {
        ingredients = new List<GameObject>();
    }
    public bool AddIngredient(GameObject ingreObj)
    {
        Ingredient ingredient = ingreObj.GetComponent<Ingredient>();
        if (ingredients.Count == 0 && ingredient.PrepType != PrepType.None)
        {
            ingredients.Add(ingreObj);
            ingreObj.transform.parent = transform;
            ingreObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            ingreObj.transform.localScale = Vector3.one * 0.75f;
            ingredientObjects = ingreObj;
            return true;
        }
        else if (ingredients.Count >= 1)
        {
            ingredients.Add(ingreObj);
            GameObject newIngredientObjects = EventManager.Instance.FindRecipeOutput(ingredients);
            if (ingredientObjects != newIngredientObjects && newIngredientObjects)
            {
                EventManager.Instance.DespawnObject(ingredientObjects.GetComponent<ItemHolder>().PoolType, ingredientObjects);
                EventManager.Instance.DespawnObject(ingredient.PoolType, ingreObj);
                ingredientObjects = EventManager.Instance.SpawnObject(newIngredientObjects.GetComponent<ItemHolder>().PoolType, Vector3.zero, Quaternion.identity, transform);
                return true;
            }
            else
            {
                ingredients.Remove(ingreObj);
                return false;
            }
        }
        return false;
    }
}
