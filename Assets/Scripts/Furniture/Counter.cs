using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 offset;
    private GameObject carriedObject = null;
    public void Interact(Transform interacterTransform, GameObject currentObject)
    {
        if (currentObject != null && carriedObject == null)
        {
            //Put object down onto the counter
            Transform currentTransform = currentObject.transform;
            currentTransform.parent = transform;
            currentTransform.localPosition = offset;
            carriedObject = currentObject;
            EventManager.Instance.ClearCarriedObject();
        }
        else if (currentObject == null && carriedObject != null)
        {
            //Pickup object from counter
            EventManager.Instance.PickupCarriedObject(carriedObject);
            carriedObject = null;

        }
        else if (currentObject == null && carriedObject == null)
        {
            Debug.Log("Holding nothing to put down on counter.");
        }
        else if (currentObject != null && carriedObject != null)
        {

            Debug.Log("Counter is full!");
        }
        
    }

    
}
