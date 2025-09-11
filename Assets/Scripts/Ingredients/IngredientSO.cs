using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Scriptable Object", menuName = "IngredientSO", order = 1)]
public class IngredientSO : ItemData
{
    [SerializeField] private Dictionary<PrepType, GameObject> prepTypes;
    [SerializeField] private PrepType currentPrep;
    [SerializeField] private PoolType poolType;
    public void SetPoolType(PoolType newPoolType)
    {
        poolType = newPoolType;
    }
    public PoolType GetPoolType()
    {
        return poolType;
    }
    public void SetCurrentPrep(PrepType newPrep)
    {
        currentPrep = newPrep;
    }
    public PrepType GetCurrentPrepType()
    {
        return currentPrep;
    }
}
public enum PrepType {
    None,
    Slice,
    Chop,
    Smash
}