using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Dishrack : MonoBehaviour, IInteractable
{
    [SerializeField] private PoolType poolType;
    private List<GameObject> containedObjects = new List<GameObject>();
    [SerializeField] private int capacity = 6;
    [SerializeField] private GameObject containerHolder;
    [SerializeField] private GameObject startPos;
    [SerializeField] private Vector3 spacing;
    [SerializeField] private Vector3 rotationOffset;
    private void Start()
    {
        InitializeContainer();
    }
    private void InitializeContainer()
    {
        Vector3 currentPos = startPos.transform.localPosition;
        for (int i = 0; i < capacity; i++)
        {
            var obj = EventManager.Instance.SpawnObject(poolType, currentPos, Quaternion.identity, containerHolder.transform);
            obj.SetActive(true);
            containedObjects.Add(obj);
            currentPos += spacing;
            obj.transform.Rotate(rotationOffset);
        }
    }
    public void Interact(Transform interacterTransform, GameObject objectToSend = null)
    {
        
        if (!objectToSend)
        {
            var obj = GetLastObject();
            if (!obj) return;
            EventManager.Instance.PickupCarriedObject(obj);
            containedObjects[containedObjects.IndexOf(obj)] = null;
            return;
        }
        if (objectToSend.TryGetComponent<Container>(out var container) && container.PoolType == poolType)
        {
            ReceiveObject(objectToSend);
            return;
        }
        
        if (!objectToSend.TryGetComponent<Ingredient>(out var ingredient)) return;
        var newObj = GetLastObject();
        if (!newObj) return;
        var newContainer =  newObj.GetComponent<Container>();
        containedObjects[containedObjects.IndexOf(newObj)] = null;
        var isAdded = newContainer.AddIngredient(ingredient.gameObject);
        if(isAdded)
            EventManager.Instance.PickupCarriedObject(newContainer.gameObject);
        
    }
    private GameObject GetLastObject()
    {
        return containedObjects.Count > 0 ? containedObjects.FindLast(v=>v!=null): null;
    }
    private void ReceiveObject(GameObject obj)
    {
        int nullIndex = -1;
        for (int i = 0; i < containedObjects.Count; i++)
        {
            if (!containedObjects[i])
            {
                nullIndex = i;
                break;
            }
        }
        if (nullIndex == -1) return;
        containedObjects[nullIndex] = obj;
        obj.transform.SetParent(containerHolder.transform);
        obj.transform.SetLocalPositionAndRotation(startPos.transform.localPosition + (nullIndex * spacing), Quaternion.identity);
        obj.transform.Rotate(rotationOffset);
        obj.SetActive(true);
        EventManager.Instance.ClearCarriedObject();
    }

}
