using UnityEngine;
public class Crate : MonoBehaviour, IInteractable
{
    [SerializeField] private PoolType poolType;
    public void Interact(Transform parent)
    {
        EventManager.Instance.SpawnObject(poolType, Vector3.up*2.5f, Quaternion.identity, parent);
    }
}
