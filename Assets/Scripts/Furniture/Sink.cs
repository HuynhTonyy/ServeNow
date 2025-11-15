using System;
using System.Collections.Generic;
using UnityEngine;

public class Sink : OperatableCounter
{
    [SerializeField] private List<ConvertableContainer> convertContainers;
    private bool processable = false;
    private PoolType typeTo = PoolType.None;
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform,currentObject);
        processable = false;
        foreach (var item in convertContainers)
        {
            var container = carriedObject.GetComponent<ItemHolder>();
            if (!container || container.PoolType != item.From) continue;
            processable = true;
            typeTo = item.To;
            break;
        }
        progressBarGroup.SetActive(processable);
    }
    public override void Operate()
    {
        if(!processable) return;    
        base.Operate();
        if (Done && carriedObject && convertContainers.Count > 0) Convert(typeTo);
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
