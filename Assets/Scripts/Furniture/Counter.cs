using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] protected Vector3 offset;
    protected GameObject carriedObject = null;
    public virtual void Interact(Transform interacterTransform, GameObject currentObject)
    {
        Container container = currentObject ? currentObject.GetComponent<Container>() : null;
        Ingredient ingredient = carriedObject ? carriedObject.GetComponent<Ingredient>() : null;
        if (currentObject && !carriedObject)
            PutDownObject(currentObject);
        else if (!currentObject && carriedObject)
        {
            EventManager.Instance.PickupCarriedObject(carriedObject);
            carriedObject = null;
        }
        else if (ingredient && container)
        {
            bool isAdded = container.AddIngredient(carriedObject);
            if (isAdded)
            {
                carriedObject = null;
            }
                
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
