using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private string prefabName;
    public void Interact(Transform parent,GameObject currentObject)
    {
        if (!currentObject)
        {
            var spawnedObj = EventManager.Instance.SpawnObject(prefabName, FolderPrefabPath.Ingredients.ToString(), parent: parent);
            EventManager.Instance.PickupCarriedObject(spawnedObj);
            return;
        }
        if (currentObject.TryGetComponent<ItemHolder>(out var itemHolder) &&  itemHolder.Name != prefabName) return;
        EventManager.Instance.DespawnObject(currentObject);
        EventManager.Instance.ClearCarriedObject();
    }
}
