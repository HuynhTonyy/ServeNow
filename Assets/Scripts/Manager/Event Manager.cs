using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Func<PoolType, Vector3, Quaternion, Transform, GameObject> onSpawnObject;
    public event Action onInteract;
    public event Action<Vector2> onInputMove;

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
    #region Object Event
    public GameObject SpawnObject(PoolType poolType, Vector3 position, Quaternion rotation, Transform transform)
    {
        return Instance.onSpawnObject?.Invoke(poolType, position, rotation, transform);
    }
    #endregion
    #region Interact
    public void Interact()
    {
        Instance.onInteract?.Invoke();
    }
    #endregion
    #region Input
    public void InputMove(Vector2 moveDir)
    {
        Instance.onInputMove?.Invoke(moveDir);
    }
    #endregion
}
