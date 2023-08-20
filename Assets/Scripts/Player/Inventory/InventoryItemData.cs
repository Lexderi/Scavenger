using MyBox;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string Id;
    public string DisplayName;
    public Sprite Sprite;
    public GameObject Prefab;
    public Vector2Int Size;
    public ItemCategory Category;
}