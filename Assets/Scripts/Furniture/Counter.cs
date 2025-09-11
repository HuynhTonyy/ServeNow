using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 offset;
    protected GameObject carriedObject = null;
    protected ItemData itemData = null;
    public virtual void Interact(Transform interacterTransform, ItemData newItemData, GameObject currentObject)
    {
        if (currentObject != null && carriedObject == null)
        {
            //Put object down onto the counter
            Transform currentTransform = currentObject.transform;
            currentTransform.parent = transform;
            currentTransform.localPosition = offset;
            carriedObject = currentObject;
            itemData = newItemData;
            EventManager.Instance.ClearCarriedObject();
        }
        else if (currentObject == null && carriedObject != null)
        {
            //Pickup object from counter
            EventManager.Instance.PickupCarriedObject(carriedObject, itemData);
            carriedObject = null;
            itemData = null;

        }
        else if (currentObject == null && carriedObject == null)
        {
            Debug.Log("Holding nothing to put down on counter.");
        }
        else if (currentObject != null && carriedObject != null)
        {

            Debug.Log("Counter is orcupied!");
        }
    }
    
}
