using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    public void Interact(Transform parent, ItemData neItemData,GameObject currentObject)
    {
        if (currentObject == null)
        {
            GameObject spawnedObj = EventManager.Instance.SpawnObject(((IngredientSO)itemData).GetPoolType(), Vector3.zero, Quaternion.identity, parent);
            EventManager.Instance.PickupCarriedObject(spawnedObj,itemData);
        }
        else
        {
            Debug.Log("Have object in hand: " + currentObject);
        }

    }
}
