using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable Object", menuName = "IngredientSO", order = 1)]
public class IngredientSO : ItemData
{
    [SerializeField] private PoolType poolType;
    public PoolType PoolType { get { return poolType; } }
    [SerializeField] private PrepType prepType;
    [SerializeField] private ProcessType processType;
    public PrepType PrepType { get { return prepType; } }
    public ProcessType ProcessType { get { return processType; } }

    public static IngredientSO Create(PoolType poolType, PrepType prepType, ProcessType  processType)
    {
        var instance = ScriptableObject.CreateInstance<IngredientSO>();
        instance.poolType = poolType;
        instance.prepType = prepType;
        instance.processType = processType;
        return instance;
    }
}
public enum PrepType {
    None,
    Slice,
    Chop,
    Smash
}
public enum ProcessType {
    None,
    Fry,
    Boil,
    Grill
}