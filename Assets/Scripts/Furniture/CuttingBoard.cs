using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : OperatableCounter
{
    [SerializeField] private List<IngredientSO> ingredientSOs;
    private bool processable = false;
    public override void Interact(Transform interacterTransform, ItemData newItemData, GameObject currentObject)
    {
        base.Interact(interacterTransform, newItemData,currentObject);
        IngredientSO ingredientSO = (IngredientSO)itemData;
        if (ingredientSO.GetCurrentPrepType() == PrepType.None)
        {
            processable = true;
        }
        else
        {
            processable = false;
        }
    }
    public override void Operate()
    {
        IngredientSO ingredientSO = (IngredientSO)itemData;
        if (carriedObject == null || itemData == null || !ingredientSOs.Contains(ingredientSO) || !processable)
        {
            Debug.Log("Can't operate");
            return;
        }
        base.Operate();
        if (done)
        {
            ingredientSO.SetCurrentPrep(PrepType.Slice);
        }
    }
}
