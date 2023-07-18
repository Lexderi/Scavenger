using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // slot references
    [SerializeField] private GameObject primarySlot;
    [SerializeField] private GameObject secondarySlot;

    // references
    private IEquipable equipedItem;
    private GameObject currentSlot;
    [SerializeField] private TMP_Text itemInformation;

    private void Start()
    {
        currentSlot = primarySlot;
        equipedItem = primarySlot.GetComponentInChildren<IEquipable>();
    }

    private void Update()
    {
        // check equip hotkeys
        if (Hotkeys.GetKeyDown("Primary")) SwitchToSlot(primarySlot);
        else if (Hotkeys.GetKeyDown("Secondary")) SwitchToSlot(secondarySlot);

        // check shoot hotkeys
        if (Hotkeys.GetKey("Shoot")) equipedItem.Use();

        // display item information
        itemInformation.text = equipedItem.HudInformation;
    }

    private void SwitchToSlot(GameObject slot)
    {
        // only switch, if it has a item in the slot
        if (slot.transform.childCount < 1) return;

        // get new item
        equipedItem = slot.GetComponentInChildren<IEquipable>();

        // deactivate old slot and activate current
        currentSlot.SetActive(false);
        slot.SetActive(true);
        currentSlot = slot;
    }
}