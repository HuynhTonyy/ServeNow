using System;
using System.Collections.Generic;
using UnityEngine;

public class Sink : OperatableCounter
{
    [SerializeField] private List<ConvertableContainer> convertContainers;
    
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform, currentObject);
    }
    public override void Operate()
    {
        PoolType typeTo = PoolType.None;
        foreach (var item in convertContainers)
        {
            ItemHolder container = carriedObject.GetComponent<ItemHolder>();
            if (container && container.PoolType == item.From)
            {
                typeTo = item.To;
                break;
            }
        }
        if(typeTo == PoolType.None)
            return;
        base.Operate();
        if (done && carriedObject && convertContainers.Count > 0)
            Convert(typeTo);
    }
    private void Convert(PoolType type)
    {
        EventManager.Instance.DespawnObject(carriedObject.GetComponent<ItemHolder>().PoolType, carriedObject);
        GameObject newObj = EventManager.Instance.SpawnObject(type,offset, Quaternion.identity, transform);
        carriedObject = newObj;
    }
}
[Serializable]
public struct ConvertableContainer
{
    [SerializeField] private PoolType from;
    public readonly PoolType From { get { return from; } }
    [SerializeField] private PoolType to;
    public readonly PoolType To { get { return to; } }

}
