using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager Instance;
    [SerializeField] private int poolSize = 5;
    private Dictionary<string, Queue<GameObject>> poolsDictionary = new ();
    private Dictionary<string, string> folderDictionary = new ();
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
        EventManager.Instance.onSpawnObjectByName += SpawnObject;
        EventManager.Instance.onSpawnObjectByPrefab += SpawnObject;
        EventManager.Instance.onDespawnObject += DespawnObject;
    }
    void OnDisable()
    {
        EventManager.Instance.onSpawnObjectByName -= SpawnObject;
        EventManager.Instance.onSpawnObjectByPrefab -= SpawnObject;
        EventManager.Instance.onDespawnObject -= DespawnObject;
    }
    private void InitializePools()
    {
        var configManager = ConfigManager.Instance;
        if(!configManager) return;
        InitializePool(configManager.ConfigContainers.Records,FolderPrefabPath.Containers.ToString());
        InitializePool(configManager.ConfigIngredients.Records,FolderPrefabPath.Ingredients.ToString());
        InitializePool(configManager.ConfigDishes.Records,FolderPrefabPath.Dishes.ToString());
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
                var newObj = Instantiate(asset, transform);
                newObj.SetActive(false);
                objectsPool.Enqueue(newObj);
            }
            poolsDictionary.Add(pool.Prefab, objectsPool);
            folderDictionary.Add(pool.Prefab, folderName);
        }
    }
    
    private GameObject SpawnObject(string prefabName,string folderName = null, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
    {
        if(!poolsDictionary.ContainsKey(prefabName))
        {
            var folerNameVerified = folderName ?? FolderPrefabPath.Others.ToString();
            folderDictionary.Add(prefabName,folerNameVerified);
            var path = $"Prefabs/{folerNameVerified}/{prefabName}";
            var newObj = Instantiate(Resources.Load<GameObject>(path), transform);
            newObj.SetActive(false);
            Queue<GameObject> newQueue = new Queue<GameObject>();
            newQueue.Enqueue(newObj);
            poolsDictionary.Add(prefabName,newQueue);
        }
        if (poolsDictionary[prefabName].Count <= 0)
        {
            var path = $"Prefabs/{folderDictionary[prefabName]}/{prefabName}";
            var newObj = Instantiate(Resources.Load<GameObject>(path), transform);
            newObj.SetActive(false);
            poolsDictionary[prefabName].Enqueue(newObj);
        }
        var obj = poolsDictionary[prefabName].Dequeue();
        obj.transform.SetParent(parent);
        obj.transform.SetLocalPositionAndRotation(position ?? Vector3.zero, rotation ?? Quaternion.identity);
        obj.SetActive(true);
        return obj;
    }
    private GameObject SpawnObject(GameObject prefab, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
    {
        var prefabName = prefab.name;
        if(!poolsDictionary.ContainsKey(prefabName))
        {
            var newObj = Instantiate(prefab, transform);
            newObj.SetActive(false);
            Queue<GameObject> newQueue = new Queue<GameObject>();
            newQueue.Enqueue(newObj);
            poolsDictionary.Add(prefabName,newQueue);
        }
        if (poolsDictionary[prefabName].Count <= 0)
        {
            var newObj = Instantiate(prefab, transform);
            newObj.SetActive(false);
            poolsDictionary[prefabName].Enqueue(newObj);
        }
        var obj = poolsDictionary[prefabName].Dequeue();
        obj.transform.SetParent(parent);
        obj.transform.SetLocalPositionAndRotation(position ?? Vector3.zero, rotation ?? Quaternion.identity);
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
