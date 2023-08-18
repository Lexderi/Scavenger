using System;
using MyBox;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Separator("Variables")]
    [SerializeField] [InitializationField] private bool isOpen;
    public bool IsOpen => isOpen;

    [Separator("References")]
    [SerializeField] private Canvas canvas;

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
}