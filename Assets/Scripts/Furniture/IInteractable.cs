using UnityEngine;
public interface IInteractable
{
    void Interact(Transform interacterTransform, GameObject objectToSend = null);
}
