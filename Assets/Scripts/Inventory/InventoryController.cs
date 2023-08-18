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
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject slotPrefab;

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

        // fill in slots
        for (int i = 0; i < gridDimensions.x * gridDimensions.y; i++)
        {
            Instantiate(slotPrefab, gridLayoutGroup.transform);
        }
    }
}