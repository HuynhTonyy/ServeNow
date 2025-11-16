using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
[Serializable]
public class ConfigContainer : Config, IPrefabConfig
{
    public string name;
    public string prefab;
    public string Prefab { get { return prefab; } }
    
}

public class ConfigContainerTable:  ConfigTable<ConfigContainer>
{
    public ConfigContainer GetConfigByPrefab(string prefab)
    {
        return Records.FirstOrDefault(v => v.prefab == prefab);
    }

    public ConfigContainer GetDirtyContainer(string prefab)
    {
        return Records.FirstOrDefault(v => v.prefab == $"dirty_{prefab}");
    }
    public ConfigContainer GetCleanContainer(string prefab)
    {
        var newPrefabName = prefab.Replace("dirty_", "");
        return Records.FirstOrDefault(v => v.prefab == newPrefabName);
    }
}