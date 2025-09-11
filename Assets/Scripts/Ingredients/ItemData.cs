using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object", order = 0)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;   
}