
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConfigManager: MonoBehaviour
{
    public static ConfigManager Instance;
    public ConfigIngredientTable ConfigIngredients = new ();
    public ConfigDishTable ConfigDishes = new ();
    public ConfigContainerTable ConfigContainers = new ();
    private void Awake()
    {
        CheckSingleton();
        LoadConfigs();
    }

    private void CheckSingleton()
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
        ConfigDishes.Records = await TsvLoader.LoadDataAsyncTsv<ConfigDish>($"{path}ConfigDish");
        ConfigContainers.Records = await TsvLoader.LoadDataAsyncTsv<ConfigContainer>($"{path}ConfigContainer");
        
    }
    
}

public class ConfigTable<T>
{
    public List<T> Records;
}