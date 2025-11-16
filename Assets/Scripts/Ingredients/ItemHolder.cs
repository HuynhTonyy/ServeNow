using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] protected string name;
    public string Name { get { return name; } set { name = value; } }
}
