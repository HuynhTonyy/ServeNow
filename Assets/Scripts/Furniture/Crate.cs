using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private PoolType poolType;
    public void Interact(Transform parent,GameObject currentObject)
    {
        if (currentObject == null)
        {
            GameObject spawnedObj = EventManager.Instance.SpawnObject(poolType, Vector3.zero, Quaternion.identity, parent);
            EventManager.Instance.PickupCarriedObject(spawnedObj);
        }
        else
        {
            Debug.Log("Have object in hand: " + currentObject);
        }

    }
}
