using System.Collections.Generic;
using UnityEngine;

public class Container : ItemHolder
{
    private List<GameObject> ingredients;
    private GameObject ingredientObjects;
    private void Start()
    {
        ingredients = new List<GameObject>();
    }
    public bool AddIngredient(GameObject ingreObj)
    {
        var ingredient = ingreObj.GetComponent<Ingredient>();
        if (!ingredient) return false;
        if (ingredients.Count == 0 && ingredient.PrepType != PrepType.None)
        {
            ingredients.Add(ingreObj);
            ingreObj.transform.parent = transform;
            ingreObj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            ingreObj.transform.localScale = Vector3.one * 0.75f;
            ingredientObjects = ingreObj;
            return true;
        }
        if (ingredients.Count < 1) return false;
        ingredients.Add(ingreObj);
        var newIngredientObjects = EventManager.Instance.FindRecipeOutput(ingredients);
        if (ingredientObjects != newIngredientObjects && newIngredientObjects)
        {
            EventManager.Instance.DespawnObject(ingredientObjects);
            EventManager.Instance.DespawnObject(ingreObj);
            ingredientObjects = EventManager.Instance.SpawnObject(newIngredientObjects.name, Vector3.zero, Quaternion.identity, transform);
            return true;
        }
        ingredients.Remove(ingreObj);
        return false;
    }
}
