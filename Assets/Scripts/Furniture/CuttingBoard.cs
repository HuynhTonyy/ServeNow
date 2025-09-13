using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : OperatableCounter
{
    private bool processable = false;
    private Ingredient ingredient;
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform,currentObject);
        if (carriedObject)
            ingredient = carriedObject.GetComponent<Ingredient>();
        else
            return;
        if (ingredient && ingredient.GetPrepType() == PrepType.None)
            processable = true;
        else
            processable = false;
    }
    public override void Operate()
    {
        if (!carriedObject|| !processable || !ingredient)
            return;
        base.Operate();
        if (done)
            ingredient.ChangePrepType(PrepType.Slice);
    }
}
