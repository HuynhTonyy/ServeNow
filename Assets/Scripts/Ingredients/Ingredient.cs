using UnityEngine;
using System.Collections.Generic;
using System;
public class Ingredient : ItemHolder
{
    private PrepType currentPrepType;
    public PrepType PrepType { get { return currentPrepType; } }
    [SerializeField] private List<PrepObj> prepObjs;
    public List<PrepObj> PrepObjs { get {return prepObjs;} }
    private GameObject currentPrepObj = null;


     void Start()
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
