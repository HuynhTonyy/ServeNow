using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class TsvLoader
{
    public static async Task<List<T>> LoadDataAsyncTsv<T>(string fileName) where T : new()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, $"{fileName}.tsv");
        #if UNITY_ANDROID
            UnityWebRequest req = UnityWebRequest.Get(path);
            await req.SendWebRequest();
            string text = req.downloadHandler.text;
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load TSV file: " + req.error);
                return list;
            }
        #else
                string text = System.IO.File.ReadAllText(path);
        #endif
        List<T> list = new List<T>();


        string[] lines = text.Split('\n');

        if (lines.Length < 2)
            return list;

        // Read header
        string[] headers = lines[0].Trim().Split('\t');

        // Reflect fields and properties of T
        var members = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance);

        // Process each row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] cols = line.Split('\t');

            T obj = new T();

            for (int col = 0; col < headers.Length && col < cols.Length; col++)
            {
                string header = headers[col];
                string value = cols[col];

                foreach (var m in members)
                {
                    if (m.Name != header) continue;

                    if (m is FieldInfo field)
                        field.SetValue(obj, ConvertValue(value, field.FieldType));

                    if (m is PropertyInfo prop && prop.CanWrite)
                        prop.SetValue(obj, ConvertValue(value, prop.PropertyType));

                    break;
                }
            }

            list.Add(obj);
        }

        return list;
    }

    private static object ConvertValue(string value, Type type)
    {
        if (type == typeof(int)) return int.Parse(value);
        if (type == typeof(float)) return float.Parse(value);
        if (type == typeof(bool)) return bool.Parse(value);
        if (type == typeof(double)) return double.Parse(value);
        if (type == typeof(string)) return value;

        return Convert.ChangeType(value, type);
    }
}
