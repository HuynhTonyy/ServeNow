using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : OperatableCounter
{
    private bool processable = false;
    private Ingredient ingredient;
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform,currentObject);
        if (!carriedObject) return;
        ingredient = carriedObject.GetComponent<Ingredient>();
        if (ingredient && ingredient.PrepType == PrepType.None)
            processable = true;
        else
            processable = false;
    }
    public override void Operate()
    {
        if (!carriedObject|| !processable || !ingredient) return;
        base.Operate();
        if (!done) return;
        ingredient.ChangePrepType(PrepType.Slice);
    }
}
