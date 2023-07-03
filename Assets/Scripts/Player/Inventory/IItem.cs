using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public Vector2 InventorySize { get; }
    public ItemCategory Category { get; }
}
