using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")] 
    public ItemInfo currentItem;
    
    public int quantity;
    public bool isFull;
    public bool isOccupied;

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    private InventoryManager manager;

    private void Awake()
    {
        manager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }
    


    public void AddItem(ItemInfo item)
    {
        if (currentItem.quantity == 0)
            currentItem = item;
        
        currentItem.quantity = item.quantity + quantity;
        quantity = currentItem.quantity;
        itemImage.sprite = currentItem.itemImage;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
    }
    private void ChangeItem(ItemInfo item)
    {
        currentItem = item;
        quantity = currentItem.quantity;
        itemImage.sprite = currentItem.itemImage;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        if(currentItem.quantity == 0) quantityText.enabled = false;
    }

    private void Update()
    {
        isOccupied = quantity > 0;
        isFull = quantity >= 10;
    }

    public void SlotSelected()
    {
        if (manager.slotSelected)
        {
            ReplaceSlot();
        }
        else
        {
            PickUpSlot();
        }
    }

    private void ReplaceSlot()
    {
        manager.slotSelected = false;
        var extra = currentItem;
        ChangeItem(manager.selectedSlot.currentItem);
        manager.selectedSlot.ChangeItem(extra);
        Debug.Log("ReplaceSlot");
    }

    private void PickUpSlot()
    {
        manager.slotSelected = true;
        manager.selectedSlot = this;
        Debug.Log("SelectSlot");
    }
}
