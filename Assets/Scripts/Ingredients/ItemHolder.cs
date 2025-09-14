using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] protected PoolType poolType;
    public PoolType PoolType { get { return poolType; } }
}
