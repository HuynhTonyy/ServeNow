using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable Object", menuName = "IngredientSO", order = 1)]
public class IngredientSO : ItemData
{
    [SerializeField] private PoolType poolType;
    public PoolType PoolType { get { return poolType; } }
    [SerializeField] private PrepType prepType;
    public PrepType PrepType { get { return prepType; } }

    public static IngredientSO Create(PoolType poolType, PrepType prepType)
    {
        var instance = ScriptableObject.CreateInstance<IngredientSO>();
        instance.poolType = poolType;
        instance.prepType = prepType;
        return instance;
    }
}
public enum PrepType {
    None,
    Slice,
    Chop,
    Smash
}