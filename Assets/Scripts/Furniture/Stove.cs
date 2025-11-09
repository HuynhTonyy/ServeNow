using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Stove : OperatableCounter
{
    private bool processable = false;
    private Ingredient ingredient;
    private ProcessType currentProcessType;
    [SerializeField] private List<ProcessType> processTypes = new List<ProcessType>();

    [SerializeField] private bool isOverHeatable;
    [SerializeField] private float overHeatTimer = 3f;
    private float currentOverHeatTimer;
    private bool isBurned = false;

    private void Start()
    {
        currentOverHeatTimer = overHeatTimer;
    }
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        isBurned = false;
        base.Interact(interacterTransform,currentObject);
        if (!carriedObject) return;
        ingredient = carriedObject.GetComponent<Ingredient>();
        processable = false;
        if(!ingredient) return;
        foreach (var type in processTypes)
        {
            if(!ingredient.CheckProcess(type)) continue;
            currentProcessType = type;
            processable =  true;
        }
    }

    protected override void Update()
    {
        if(!processable) return;
        base.Update();
        if(!done) return;
        ingredient.ChangeProcessType(currentProcessType);
        if(!isOverHeatable) return;
        currentOverHeatTimer = Math.Max(currentOverHeatTimer-Time.deltaTime,0f);
        if (currentOverHeatTimer > 0f) return;
        if(isBurned) return;
        isBurned = true;
        EventManager.Instance.DespawnObject(carriedObject.GetComponent<ItemHolder>().PoolType,carriedObject);
        EventManager.Instance.SpawnObject(PoolType.Trash, offset,Quaternion.identity,this.transform);
    }

    public override void Operate()
    {
        
    }
}
