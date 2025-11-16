using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
[Serializable]
public class ConfigDish : Config, IPrefabConfig
{
    public string name;
    public string prefab;
    public string Prefab { get { return prefab; } }
}

public class ConfigDishTable:  ConfigTable<ConfigDish>
{
        
    public ConfigDish GetConfigByPrefab(string prefab)
    {
        return Records.FirstOrDefault(v => v.prefab == prefab);
    }
}