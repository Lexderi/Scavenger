using System;
using MyBox;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Separator("Variables")]
    [SerializeField] [InitializationField] private bool isOpen;
    public bool IsOpen => isOpen;

    [Separator("References")]
    [SerializeField] private Canvas inventoryCanvas;

    private void Start()
    {
        inventoryCanvas.gameObject.SetActive(isOpen);
    }

    private void Update()
    {
        // check hotkey
        if (Hotkeys.GetKeyDown("ToggleInventory")) ToggleInventory();
    }

    private void ToggleInventory()
    {
        isOpen = !IsOpen;
        inventoryCanvas.gameObject.SetActive(isOpen);
    }
}