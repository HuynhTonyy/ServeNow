using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 offset;
    protected GameObject carriedObject = null;
    public virtual void Interact(Transform interacterTransform, GameObject currentObject)
    {
        //Put down when nothing on counter
        if (currentObject && !carriedObject)
        {
            PutDownObject(currentObject);
            return;
        }
        //Pick up when nothing on hand
        if (!currentObject && carriedObject)
        {
            EventManager.Instance.PickupCarriedObject(carriedObject);
            carriedObject = null;
            return;
        }
        //Combine dish 
        var heldContainer = carriedObject ? carriedObject.GetComponent<Container>() : null;
        var targetContainer = currentObject ? currentObject.GetComponent<Container>() : null;

        var heldIngredient = carriedObject ? carriedObject.GetComponent<Ingredient>() : null;
        var targetIngredient = currentObject ? currentObject.GetComponent<Ingredient>() : null;
        var isAdded = false;

        // Case 1: Holding ingredient, looking at container
        if (heldContainer && targetIngredient)
        {
            isAdded = heldContainer.AddIngredient(currentObject);
            if (isAdded) 
            {
                EventManager.Instance.ClearCarriedObject();
            }
        }

        // Case 2: Holding container, looking at ingredient
        else if (heldIngredient && targetContainer)
        {
            isAdded = targetContainer.AddIngredient(carriedObject);
            if (isAdded) carriedObject = null;
        }

        if (isAdded)
        {
            // Play sound, animation, etc.
        }

    }
    private void PutDownObject(GameObject obj)
    {
        Transform currentTransform = obj.transform;
        currentTransform.parent = transform;
        currentTransform.localPosition = offset;
        carriedObject = obj;
        EventManager.Instance.ClearCarriedObject();
    }
}
