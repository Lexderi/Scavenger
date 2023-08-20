using MyBox;
using TMPro;
using UnityEngine;
using Zenject;

public class EquipController : MonoBehaviour
{
    // slot references
    [SerializeField] private GameObject primarySlot;
    [SerializeField] private GameObject secondarySlot;

    // references
    private IEquipable equippedItem;
    private GameObject currentSlot;
    [SerializeField] private TMP_Text itemInformation;

    [Inject] private readonly InventoryController mInventory;

    private void Start()
    {
        currentSlot = primarySlot;
        equippedItem = primarySlot.GetComponentInChildren<IEquipable>();
    }

    private void Update()
    {
        if (mInventory.IsOpen) return;

        // check equip hotkeys
        if (Hotkeys.GetKeyDown("Primary")) SwitchToSlot(primarySlot);
        else if (Hotkeys.GetKeyDown("Secondary")) SwitchToSlot(secondarySlot);

        // check shoot hotkeys
        if (Hotkeys.GetKey("Shoot")) equippedItem.Use();

        // display item information
        itemInformation.text = equippedItem.HudInformation;
    }

    private void SwitchToSlot(GameObject slot)
    {
        // only switch, if it has a item in the slot
        if (slot.transform.childCount < 1) return;

        // get new item
        equippedItem = slot.GetComponentInChildren<IEquipable>();

        // deactivate old slot and activate current
        currentSlot.SetActive(false);
        slot.SetActive(true);
        currentSlot = slot;
    }

    [ButtonMethod]
    private void TestInventory()
    {
        mInventory.TryAddItem(Vector2Int.zero, equippedItem);
    }
}