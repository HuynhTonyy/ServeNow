using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager Instance;
    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<PoolType, Queue<GameObject>> poolsDictionary = new Dictionary<PoolType, Queue<GameObject>>();
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
    private void Start()
    {

    }
    private void OnEnable()
    {
        EventManager.Instance.onSpawnObject += SpawnObject;
        EventManager.Instance.onDespawnObject += DespawnObject;
    }
    void OnDisable()
    {
        EventManager.Instance.onSpawnObject += SpawnObject;
        EventManager.Instance.onDespawnObject -= DespawnObject;
    }
    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObj = Instantiate(pool.prefab);
                newObj.SetActive(false);
                objectsPool.Enqueue(newObj);
            }
            poolsDictionary.Add(pool.poolType, objectsPool);
        }
    }
    private GameObject SpawnObject(PoolType poolType, Vector3 position, Quaternion rotation, Transform parrent = null)
    {
        if (!poolsDictionary.ContainsKey(poolType))
        {
            Debug.Log("Pool type not found! - " + poolType);
            return null;
        }
        if (poolsDictionary[poolType].Count <= 0)
        {
            Debug.Log("No more object to use");
            return null;
        }
        GameObject obj = poolsDictionary[poolType].Dequeue();
        obj.SetActive(true);
        if (parrent != null)
        {
            obj.transform.parent = parrent;
        }
        else
        {
            obj.transform.parent = transform;
        }
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        return obj;
    }
    private void DespawnObject(PoolType poolType,GameObject storeObject)
    {
        storeObject.transform.parent = transform;
        storeObject.transform.position = Vector3.zero;
        storeObject.transform.rotation = Quaternion.identity;
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
    Lecttuce,
    Potato,
    Tomato,
    Onion,
    Plate,
    Bowl,
    DirtyPlate,
    DirtyBowl,
}
[System.Serializable]
public struct Pool
{
    public PoolType poolType;
    public int size;
    public GameObject prefab;
}
