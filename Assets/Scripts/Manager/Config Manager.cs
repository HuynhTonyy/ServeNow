
using System.Collections.Generic;
using UnityEngine;
public class ConfigManager: MonoBehaviour
{
    public static ConfigManager Instance;
    private ConfigIngredientTable ConfigIngredients = new ConfigIngredientTable();
    private void Awake()
    {
        CheckSigleton();
        LoadConfigs();
    }

    private void CheckSigleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return; 
        }
        Destroy(gameObject);
    }
    private async void LoadConfigs()
    {
        var path = "Config/";
        ConfigIngredients.Records = await TsvLoader.LoadDataAsyncTsv<ConfigIngredient>($"{path}ConfigIngredient");
    }
    
}

public class ConfigTable<T>
{
    public List<T> Records;
}