using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Func<PoolType, Vector3, Quaternion, Transform, GameObject> onSpawnObject;
    public event Action<PoolType, GameObject> onDespawnObject;
    public event Action onInteract;
    public event Action<Vector2> onInputMove;
    public event Action<GameObject> onPickUpCarriedObject;
    public event Action onClearCrarriedObject;
    public event Action onOperate;
    public event Func<List<GameObject>, GameObject> onFindRecipeOutput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #region Pooling Event
    public GameObject SpawnObject(PoolType poolType, Vector3 position, Quaternion rotation, Transform transform)
    {
        return Instance.onSpawnObject?.Invoke(poolType, position, rotation, transform);
    }
    public void DespawnObject(PoolType poolType, GameObject gameObject)
    {
        Instance.onDespawnObject?.Invoke(poolType, gameObject);
    }
    #endregion
    #region Input Event
    public void InputInteract()
    {
        Instance.onInteract?.Invoke();
    }
    public void InputMove(Vector2 moveDir)
    {
        Instance.onInputMove?.Invoke(moveDir);
    }
    public void InputOperate()
    {
        Instance.onOperate?.Invoke();
    }
    #endregion
    #region Interact Event
    public void PickupCarriedObject(GameObject gameObject)
    {
        Instance.onPickUpCarriedObject?.Invoke(gameObject);
    }
    public void ClearCarriedObject()
    {
        Instance.onClearCrarriedObject?.Invoke();
    }
    #endregion
    #region Dish Event
    public GameObject FindRecipeOutput(List<GameObject> ingredients)
    {
        return Instance.onFindRecipeOutput?.Invoke(ingredients);
    }
    #endregion
}
