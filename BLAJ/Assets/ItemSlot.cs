using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")] 
    
    public ItemInfo currentItem;
    
    public int slotQuantity;
    public bool isFull;
    public bool isOccupied;

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    


    public void AddItem(ItemInfo item)
    {
        if (currentItem.quantity == 0)
            currentItem = item;
        
        slotQuantity = item.quantity + slotQuantity;
        UpdateInfo(slotQuantity);
    }
    public void UpdateInfo(int Quantity)
    {
        itemImage.sprite = currentItem.itemImage;
        slotQuantity = Quantity;
        quantityText.text = slotQuantity.ToString();
        quantityText.enabled = true;
        if(currentItem.quantity == 0) quantityText.enabled = false;
    }

    private void Update()
    {
        isOccupied = slotQuantity > 0;
        isFull = slotQuantity >= 10;
    }
}
