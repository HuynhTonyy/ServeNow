using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 offset;
    protected GameObject carriedObject = null;
    public virtual void Interact(Transform interacterTransform, GameObject currentObject)
    {

        if (currentObject && !carriedObject)
            PutDownObject(currentObject);
        else if (!currentObject && carriedObject)
        {
            EventManager.Instance.PickupCarriedObject(carriedObject);
            carriedObject = null;
        }
        else if (currentObject && carriedObject)
            TryPlaceIngredientOntoDish(currentObject);
    }
    private void PutDownObject(GameObject obj)
    {
        Transform currentTransform = obj.transform;
        currentTransform.parent = transform;
        currentTransform.localPosition = offset;
        carriedObject = obj;
        EventManager.Instance.ClearCarriedObject();
    }
    private void TryPlaceIngredientOntoDish(GameObject currentObject)
    {
        if (currentObject.TryGetComponent<Container>(out var containerFromCurrent) &&
                carriedObject.GetComponent<Ingredient>())
            {
                bool isAdded = containerFromCurrent.AddIngredient(carriedObject);
                if (isAdded)
                    carriedObject = null;
            }
            else if (carriedObject.TryGetComponent<Container>(out var containerFromCarried) &&
                currentObject.GetComponent<Ingredient>())
            {
                bool isAdded = containerFromCarried.AddIngredient(currentObject);
                if(isAdded)
                    EventManager.Instance.ClearCarriedObject();
            }
    }
}
