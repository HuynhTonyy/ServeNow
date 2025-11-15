using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
[Serializable]
public class ConfigIngredient : Config
{
    public string name;
    public string prefab;
}

public class ConfigIngredientTable:  ConfigTable<ConfigIngredient>
{
        
    public ConfigIngredient GetConfigByPrefab(string prefab)
    {
        return Records.FirstOrDefault(v => v.prefab == prefab);
    }
}