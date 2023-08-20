using UnityEngine;

public interface IItem
{
    public InventoryItemData InventoryData { get; }

    public GameObject GetGameObject();
}