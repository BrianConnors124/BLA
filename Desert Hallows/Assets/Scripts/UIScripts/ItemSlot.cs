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
    public bool isOccupied => !currentItem.itemName.Equals("");

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    


    public void AddItem(ItemInfo item, int quantity)
    {
        if (slotQuantity == 0)
            currentItem = item;
        
        slotQuantity += quantity;
        UpdateInfo(slotQuantity);
    }
    public void UpdateInfo(int newSlotCount)
    {
        slotQuantity = newSlotCount;
        itemImage.sprite = currentItem.itemImage;
        quantityText.text = slotQuantity.ToString();
        quantityText.enabled = true;
        if(slotQuantity == 0) quantityText.enabled = false;
    }
}
