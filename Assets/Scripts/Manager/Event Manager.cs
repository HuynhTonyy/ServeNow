using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Func<PoolType,Vector3,Quaternion,GameObject> onSpawnObject;
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
    public GameObject SpawnObject(PoolType poolType,Vector3 position,Quaternion rotation)
    {
        return Instance.onSpawnObject?.Invoke(poolType,position,rotation);
    }
    #endregion
}
