using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] protected PoolType poolType;
    public PoolType GetPoolType()
    {
        return poolType;
    }
    protected virtual void Start() {
        
    }
}
