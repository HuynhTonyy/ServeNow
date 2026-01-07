using System;
using System.Collections.Generic;
using UnityEngine;

public class Sink : OperatableCounter
{
    private bool processable = false;
    private ConfigContainer container = null;
    public override void Interact(Transform interacterTransform, GameObject currentObject)
    {
        base.Interact(interacterTransform,currentObject);
        var name = carriedObject.GetComponent<ItemHolder>().Name;
        container = ConfigManager.Instance.ConfigContainers.GetCleanContainer(name);
        progressBarGroup.SetActive(container != null);
    }
    public override void Operate()
    {
        if(!processable) return;    
        base.Operate();
        if (Done && carriedObject) Convert();
    }
    private void Convert()
    {
        EventManager.Instance.DespawnObject(carriedObject);
        var newObj = EventManager.Instance.SpawnObject(container.prefab, FolderPrefabPath.Containers.ToString(),position:offset, parent: transform);
        carriedObject = newObj;
        container = null;
    }
}
