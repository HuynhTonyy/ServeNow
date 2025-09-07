using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "RecipeSO", order = 0)]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private int price;
    [SerializeField] private List<IngredientSO> ingredients;
    [SerializeField] private GameObject output;
    public List<IngredientSO> GetIngredientSOs()
    {
        return ingredients;
    }
}
