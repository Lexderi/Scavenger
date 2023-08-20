using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [Separator("References")]
    [SerializeField] private InventoryItemData data;

    [MustBeAssigned] [SerializeField] private Image image;
    [MustBeAssigned] [SerializeField] private TMP_Text nameText;
    [MustBeAssigned] [SerializeField] private TMP_Text categoryText;

    [HideInInspector] public GameObject Item;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public InventoryItemData Data
    {
        get => data;

        set
        {
            data = value;
            UpdateDisplay();
        }
    }

    [ButtonMethod]
    private void UpdateDisplay()
    {
        if(rectTransform == null) rectTransform = GetComponent<RectTransform>();

        image.sprite = data.Sprite;
        nameText.text = data.DisplayName;
        categoryText.text = data.Category.ToString().Replace('_', ' ');

        rectTransform.sizeDelta = data.Size * 60;
    }
}
