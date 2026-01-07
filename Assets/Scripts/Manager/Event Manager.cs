using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Func<string, string, Vector3?, Quaternion?, Transform,GameObject>  onSpawnObjectByName;
    public event Func<GameObject, Vector3?, Quaternion?, Transform,GameObject>  onSpawnObjectByPrefab;
    public event Action<GameObject> onDespawnObject;
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
    public GameObject SpawnObject(string prefabName, string folderName, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
    {
        return Instance.onSpawnObjectByName?.Invoke(prefabName,folderName, position, rotation, parent);
    }
    public GameObject SpawnObject(GameObject prefab, Vector3? position = null, Quaternion? rotation = null, Transform transform = null)
    {
        return Instance.onSpawnObjectByPrefab?.Invoke(prefab, position, rotation, transform);
    }
    public void DespawnObject(GameObject gameObject)
    {
        Instance.onDespawnObject?.Invoke(gameObject);
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
