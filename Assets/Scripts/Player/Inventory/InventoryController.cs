using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject primarySlot;
    [SerializeField] private GameObject secondarySlot;

    private IEquipable equipedItem;
    private GameObject currentSlot;

    private void Update()
    {
        // check hotkeys
        if(Hotkeys.GetKey("Primary")) SwitchToSlot(primarySlot);
    }

    private void SwitchToSlot(GameObject slot)
    {
        // only switch, if it has a primary
        if (slot.transform.childCount < 1) return;

        // get new item
        equipedItem = slot.GetComponentInChildren<IEquipable>();

        // deactivate old slot and activate current
        currentSlot.SetActive(false);
        slot.SetActive(true);
        currentSlot = slot;
    }
}
