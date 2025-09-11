using UnityEngine;
public interface IInteractable
{
    void Interact(Transform interacterTransform, ItemData itemData = null, GameObject objectToSend = null);
}
