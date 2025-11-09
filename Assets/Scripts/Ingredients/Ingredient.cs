using UnityEngine;
using System.Collections.Generic;
using System;
public class Ingredient : ItemHolder
{
    private PrepType currentPrepType;
    private ProcessType currentProcessType;
    public PrepType PrepType { get { return currentPrepType; } }
    public ProcessType ProcessType { get { return currentProcessType; } }
    [SerializeField] private List<PrepObj> prepObjs;
    [SerializeField] private List<ProcessObj> processObjs;
    private GameObject currentPrepObj = null;

    public GameObject CheckProcess(ProcessType  processType)
    {
        return processObjs.Find(v=>
            v.GetProcessType() == processType && 
            v.GetPrepType() == currentPrepType).GetPrefab();
    }

     private void Start()
    {
        currentPrepObj = prepObjs.Find(v => v.GetPrepType() == currentPrepType).GetPrefab();
        ChangePrepType(PrepType.None);
        ChangeProcessType(ProcessType.None);
    }
    public void ChangePrepType(PrepType newPrepType)
    {
        if(newPrepType == currentPrepType) return;
        currentPrepType = newPrepType;
        if (currentPrepObj)
            currentPrepObj.SetActive(false);
        currentPrepObj = prepObjs.Find(v => v.GetPrepType() == currentPrepType).GetPrefab();
        currentPrepObj.SetActive(true);
    }
    public void ChangeProcessType(ProcessType newProcessType)
    {
        if(newProcessType ==  currentProcessType) return;
        currentProcessType = newProcessType;
        if (currentPrepObj)
            currentPrepObj.SetActive(false);
        currentPrepObj = processObjs.Find(v => v.GetProcessType() == currentProcessType).GetPrefab();
        currentPrepObj.SetActive(true);
    }
}
[Serializable]
public struct PrepObj
{
    [SerializeField] private PrepType prepType;
    [SerializeField] private GameObject prefab;
    public PrepType GetPrepType(){return prepType;}
    public GameObject GetPrefab(){return prefab;}

}
[Serializable]
public struct ProcessObj
{
    [SerializeField] private PrepType prepType;
    [SerializeField] private ProcessType processType;
    [SerializeField] private GameObject prefab;
    public PrepType GetPrepType(){return prepType;}
    public ProcessType GetProcessType(){return processType;}
    public GameObject GetPrefab(){return prefab;}

}
