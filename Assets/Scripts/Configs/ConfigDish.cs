using System;
using System.Collections.Generic;
using System.Linq;
[Serializable]
public class ConfigDish : Config, IPrefabConfig
{
    public string name;
    public string prefab;
    public string ingredients;
    public string preps;
    public string procs;
    public string Prefab { get { return prefab; } }
}

public class ConfigDishTable:  ConfigTable<ConfigDish>
{

    public ConfigDish GetConfigByPrefab(string prefab)
    {
        return Records.FirstOrDefault(v => v.prefab == prefab);
    }
    public List<ConfigDish> GetAll()
    {
        return Records;
    }
    private static string[] Normalize(string[] arr)
    {
        return arr?
            .Select(s => s.Trim().ToLower())
            .Where(s => !string.IsNullOrEmpty(s))
            .OrderBy(s => s)
            .ToArray() ?? Array.Empty<string>();
    }

    private static string[] Normalize(string str)
    {
        return str?
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToLower())
            .OrderBy(s => s)
            .ToArray() ?? Array.Empty<string>();
    }

    public ConfigDish GetConfigByIngPrepProc(
    string[] ingredients,
    string[] preps,
    string[] procs)
    {
        var sortedIngredients = Normalize(ingredients);
        var sortedPreps = Normalize(preps);
        var sortedProcs = Normalize(procs);

        return Records.FirstOrDefault(v =>
            sortedIngredients.SequenceEqual(Normalize(v.ingredients)) &&
            sortedPreps.SequenceEqual(Normalize(v.preps)) &&
            sortedProcs.SequenceEqual(Normalize(v.procs))
        );
    }

}