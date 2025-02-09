using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Controls;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")] 
    
    public ItemInfo currentItem;
    
    public int slotQuantity;
    public bool isOccupied => !currentItem.itemName.Equals("");

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public InventoryManager manager;


    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


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
    
    private void UpdateInfo()
    {
        if (slotQuantity == 0)
        {
            quantityText.enabled = false;
            currentItem = manager.emptyItem;
        }
        else
        {
            quantityText.text = slotQuantity.ToString();
            quantityText.enabled = true;
        }
        
        itemImage.sprite = currentItem.itemImage;
        InputSystemController.instance.updateDescription.Invoke();
    }


    public void UseItem()
    {
        print("Use Item" + gameObject.name);
        #region Heart

        if (currentItem.itemName.ToUpper().Equals("THE HEART") && player.health != player.maxHealth)
        {
            player.health += 50;
            if (player.health > player.maxHealth) player.health = player.maxHealth;
            slotQuantity--;
            UpdateInfo();
        }
        

        #endregion
        
        
    }
}
