using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : OperatableCounter
{
    private bool processable = false;
    private ItemHolder itemHolder;
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform,currentObject);
        if (carriedObject)
            itemHolder = carriedObject.GetComponent<ItemHolder>();
        else
            return;
        if (itemHolder.GetPrepType() == PrepType.None)
            processable = true;
        else
            processable = false;
    }
    public override void Operate()
    {
        if (carriedObject == null || !processable)
            return;
        base.Operate();
        if (done)
            itemHolder.ChangePrepType(PrepType.Slice);
    }
}
