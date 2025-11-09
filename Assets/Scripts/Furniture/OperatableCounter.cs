using System;
using System.Collections;
using UnityEngine;

public abstract class OperatableCounter : Counter
{
    [SerializeField] private float operateTime;
    private float operateTimeLeft = 0;
    protected bool done = false;
    [Header("Auto")]
    [SerializeField] private bool isAutoOperate = false;
    [SerializeField] private float autoOperateMultiplier = 1f;
    [Header("Deplet")]
    [SerializeField] private bool isDepletOvertime =  false;
    [SerializeField] private float waitBeforeDepletTime = 0.5f;
    [SerializeField] private float depletMultiplier =  1f;
    private float currentWaitBeforeDepletTime;

    private void Start()
    {
        currentWaitBeforeDepletTime = waitBeforeDepletTime;
    }

    protected virtual void Update()
    {
        if(done) return;
        if(!carriedObject) return;
        if (isAutoOperate)
        {
            OperateTime(autoOperateMultiplier);
            return;
        }
        if(operateTimeLeft <= 0f) return;
        if(!isDepletOvertime) return;
        Deplet();
    }

    private void Deplet()
    {
        if(currentWaitBeforeDepletTime > 0f)
            currentWaitBeforeDepletTime -=  Time.deltaTime;
        else
            operateTimeLeft = Math.Max(operateTimeLeft - Time.deltaTime * depletMultiplier, 0f);
    }
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform, currentObject);
        done = false;
        operateTimeLeft = 0;
    }
    public virtual void Operate()
    {
        if (!carriedObject || done) return;
        currentWaitBeforeDepletTime = waitBeforeDepletTime;
        OperateTime();
    }

    private void OperateTime(float multiplier = 1f)
    {
        operateTimeLeft = Math.Min(operateTimeLeft + Time.deltaTime * multiplier, operateTime);
        if (operateTime > operateTimeLeft) return;
        done = true;
        operateTimeLeft = operateTime;
    }
}
