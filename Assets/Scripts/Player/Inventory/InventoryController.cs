using System;
using MyBox;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Separator("Variables")]
    [SerializeField] [InitializationField] private bool isOpen;
    public bool IsOpen => isOpen;
    [SerializeField] [InitializationField] [PositiveValueOnly] private Vector2Int gridDimensions;

    [Separator("References")]
    [SerializeField] private Canvas canvas;

    [SerializeField] private Transform inventorySlotContainer;
    [SerializeField] private Transform itemStash;
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject itemDisplayPrefab;

    private bool[,] occupationMatrix;

    private void Awake()
    {
        gridLayoutGroup = inventorySlotContainer.GetComponent<GridLayoutGroup>();

        // generate occupation matrix
        occupationMatrix = new bool[gridDimensions.x, gridDimensions.y];
    }

    private void Start()
    {
        canvas.gameObject.SetActive(isOpen);
    }

    private void Update()
    {
        // check hotkey
        if (Hotkeys.GetKeyDown("ToggleInventory")) ToggleInventory();
    }

    private void ToggleInventory()
    {
        isOpen = !IsOpen;
        canvas.gameObject.SetActive(isOpen);
    }

    [ButtonMethod]
    private void GenerateSlots()
    {
        if(gridLayoutGroup == null) gridLayoutGroup = inventorySlotContainer.GetComponent<GridLayoutGroup>();

        // clear slots (not that easy because of edit mode and array order and stuff)
        GameObject[] tempArray = new GameObject[gridLayoutGroup.transform.childCount];
        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = gridLayoutGroup.transform.GetChild(i).gameObject;
        }
        foreach (GameObject child in tempArray)
        {
            DestroyImmediate(child);
        }

        // set column constraint
        gridLayoutGroup.constraintCount = gridDimensions.x;

        // generate occupation matrix
        occupationMatrix = new bool[gridDimensions.x, gridDimensions.y];

        // fill in slots
        for (int i = 0; i < gridDimensions.x * gridDimensions.y; i++)
        {
            Instantiate(slotPrefab, gridLayoutGroup.transform);
        }
    }

    public bool TryAddItem(Vector2Int position, IItem item)
    {
        InventoryItemData data = item.InventoryData;

        // check if it can fit
        if (!CanFit(position, data.Size)) return false;

        // mark spot as occupied
        for (int i = position.x; i < data.Size.x; i++)
        {
            for (int j = position.y; j < data.Size.y; j++)
            {
                occupationMatrix[i, j] = true;
            }
        }

        // get inventory slot at position
        Transform slot = inventorySlotContainer.GetChild(position.x + position.y * (gridDimensions.x - 1));

        // add item to slot
        ItemController itemDisplay = Instantiate(itemDisplayPrefab, slot.GetChild(1)).GetComponent<ItemController>();

        // move item to stash
        GameObject itemGameObject = item.GetGameObject();
        itemGameObject.transform.parent = itemStash.transform;
        itemGameObject.SetActive(false);

        // set variables in item display
        itemDisplay.Data = data;
        itemDisplay.Item = itemGameObject;
        
        return true;
    }

    private bool CanFit(Vector2Int position, Vector2Int size)
    {
        // checks if it goes out of bounds
        if(position.x < 0 || position.y < 0 ||
           (position.x + size.x) >= occupationMatrix.GetLength(0) || (position.y + size.y) >= occupationMatrix.GetLength(1)) return false;

        // check if one of the slots is occupied
        for (int i = position.x; i < size.x; i++)
        {
            for (int j = position.y; j < size.y; j++)
            {
                if (occupationMatrix[i, j]) return false;
            }
        }

        return true;
    }
}