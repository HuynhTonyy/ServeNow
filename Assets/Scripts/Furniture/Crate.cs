using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private PoolType poolType;
    public void Interact(Transform parent,GameObject currentObject)
    {
        if (!currentObject)
        {
            GameObject spawnedObj = EventManager.Instance.SpawnObject(poolType, Vector3.zero, Quaternion.identity, parent);
            EventManager.Instance.PickupCarriedObject(spawnedObj);
        }
        else if (currentObject.TryGetComponent<ItemHolder>(out var itemHolder) && itemHolder.PoolType == poolType)
        {
            EventManager.Instance.DespawnObject(poolType, currentObject);
            EventManager.Instance.ClearCarriedObject();
        }
    }
}
