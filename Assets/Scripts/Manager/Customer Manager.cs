using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject customerPrefab;

    private CustomerManager Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Spawn(1);
    }
    private void Spawn(int num)
    {
        for (int i = 0; i < num; i++)
        {
            EventManager.Instance.SpawnObject(customerPrefab, transform: spawnPoint);
        }
    }
}