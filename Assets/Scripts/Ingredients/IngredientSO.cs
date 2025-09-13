using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable Object", menuName = "IngredientSO", order = 1)]
public class IngredientSO : ItemData
{
    [SerializeField] private PoolType poolType;
}
public enum PrepType {
    None,
    Slice,
    Chop,
    Smash
}