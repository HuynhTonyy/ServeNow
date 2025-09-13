using System.Collections;
using UnityEngine;

public abstract class OperatableCounter : Counter
{
    [SerializeField] float operateTime;
    float operateTimeLeft = 0;
    protected bool done = false;
    protected IOperate operatedObj = new IOperate();
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform, currentObject);
        done = false;
        operateTimeLeft = 0;
    }
    public virtual void Operate()
    {
        if (!done)
        {
            operateTimeLeft += Time.deltaTime;
        }
        if (carriedObject != null && operateTime <= operateTimeLeft && !done)
        {
            done = true;
            operateTimeLeft = operateTime;
        }
    }
}
