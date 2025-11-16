using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager Instance;
    [SerializeField] private int poolSize = 5;
    private Dictionary<string, Queue<GameObject>> poolsDictionary = new ();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        EventManager.Instance.onSpawnObject += SpawnObject;
        EventManager.Instance.onDespawnObject += DespawnObject;
    }
    void OnDisable()
    {
        EventManager.Instance.onSpawnObject -= SpawnObject;
        EventManager.Instance.onDespawnObject -= DespawnObject;
    }
    private void InitializePools()
    {
        var configManager = ConfigManager.Instance;
        if(!configManager) return;
        InitializePool(configManager.ConfigContainers.Records,"Containers");
        InitializePool(configManager.ConfigIngredients.Records,"Ingredients");
        InitializePool(configManager.ConfigDishes.Records,"Dishes");
    }

    private void InitializePool<T>(List<T> list, string folderName) where T : Config, IPrefabConfig
    {
        if(list.Count <= 0) return;
        foreach (var  pool  in list)
        {
            var objectsPool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                var path = $"Prefabs/{folderName}/{pool.Prefab}";
                var asset = Resources.Load<GameObject>(path);
                Debug.Log(path);
                var newObj = Instantiate(asset, transform);
                newObj.SetActive(false);
                objectsPool.Enqueue(newObj);
            }
            poolsDictionary.Add(pool.Prefab, objectsPool);
        }
    }
    
    private GameObject SpawnObject(string prefabName, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolsDictionary.ContainsKey(prefabName))
        {
#if UNITY_EDITOR
            Debug.Log("Pool type not found! - " + prefabName);
#endif
            return null;
        }
        if (poolsDictionary[prefabName].Count <= 0)
        {
            var newObj = Instantiate(Resources.Load<GameObject>(prefabName), transform);
            newObj.SetActive(false);
            poolsDictionary[prefabName].Enqueue(newObj);
#if UNITY_EDITOR
            Debug.Log($"Create new {prefabName}");
#endif
        }
        var obj = poolsDictionary[prefabName].Dequeue();
        obj.transform.parent = parent ? parent : transform;
        obj.transform.SetLocalPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }
    private void DespawnObject(GameObject storeObject)
    {
        var poolType =  storeObject.name.Replace("(Clone)", "");
        storeObject.transform.parent = transform;
        storeObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        if (!poolsDictionary.ContainsKey(poolType))
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            newQueue.Enqueue(storeObject);
            poolsDictionary.Add(poolType, newQueue);
        }
        else
        {
            poolsDictionary[poolType].Enqueue(storeObject);
        }
        storeObject.SetActive(false);
    }
}
public enum PoolType
{
    None,
    Lettuce,
    Potato,
    Tomato,
    Onion,
    Plate,
    Bowl,
    DirtyPlate,
    DirtyBowl,
    Salad,
    Customer,
    Trash
}
[System.Serializable]
public struct Pool
{
    public PoolType poolType;
    public int size;
    public GameObject prefab;
}
