using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName;
    public Sprite itemSprite;
    public int quantity;
    public bool isFull;

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;


    public void AddItem(string itemName, int quantity, Sprite image)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        itemImage.sprite = image;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
    }
}
