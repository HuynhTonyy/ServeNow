using System;
using System.Collections.Generic;
using UnityEngine;
public enum PoolType
{
    Lecttuce,
}
[System.Serializable]
public struct Pool
{
    public PoolType poolType;
    public int size;
    public GameObject prefab;
}
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
    }
    void OnDisable()
    {
        EventManager.Instance.onSpawnObject -= SpawnObject;
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
    private GameObject SpawnObject(PoolType poolType, Vector3 position, Quaternion rotation)
    {
        if (!poolsDictionary.ContainsKey(poolType))
        {
            Debug.Log("Pool type not found! - " + poolType);
            return null;
        }
        GameObject obj = poolsDictionary[poolType].Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }
}
