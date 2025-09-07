using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable Object", menuName = "IngredientSO", order = 1)]
public class IngredientSO : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;
    [SerializeField] private PrepType prepType;
    [SerializeField] private PoolType poolType;
    [SerializeField] private CookState cookState;
}
public enum PrepType {
    None,
    Slice,
    Chop,
    Smash
}
public enum CookState {
    Fresh,
    Cook,
    Burn
}