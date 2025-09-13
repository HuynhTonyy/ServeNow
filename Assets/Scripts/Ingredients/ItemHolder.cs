using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private PoolType poolType;
    private PrepType currentPrepType;
    [SerializeField] private List<PrepObj> prepObjs;
    private GameObject currentPrepObj = null;
    public PoolType GetPoolType()
    {
        return poolType;
    }
    public PrepType GetPrepType()
    {
        return currentPrepType;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void Start()
    {
        ChangePrepType(PrepType.None);
    }
    public void ChangePrepType(PrepType newPrepType)
    {
        currentPrepType = newPrepType;
        if (currentPrepObj != null)
        {
            currentPrepObj.SetActive(false);
        }
        foreach (var item in prepObjs)
        {
            if (item.GetPrepType() == currentPrepType)
            {
                currentPrepObj = item.GetPrefab();
                break;
            }
        }
        currentPrepObj.SetActive(true);
    }
}
[Serializable]
public struct PrepObj
{
    [SerializeField] private PrepType prepType;
    [SerializeField] private GameObject prefab;
    public PrepType GetPrepType()
    {
        return prepType;
    }
    public GameObject GetPrefab()
    {
        return prefab;
    }
}
