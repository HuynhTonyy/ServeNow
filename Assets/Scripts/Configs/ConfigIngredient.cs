using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
[Serializable]
public class ConfigIngredient : Config, IPrefabConfig
{
    public string name;
    public string prefab;
    public string Prefab => prefab;
}

public class ConfigIngredientTable:  ConfigTable<ConfigIngredient>
{
        
    public ConfigIngredient GetConfigByPrefab(string prefab)
    {
        return Records.FirstOrDefault(v => v.Prefab == prefab);
    }
}