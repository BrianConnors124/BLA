using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Item Data")] 
    
    public ItemInfo currentItem;
    
    public int slotQuantity;
    public bool isOccupied => !currentItem.itemName.Equals("");

    [Header("Item Slot")] 
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public InventoryManager manager;

    public EventSystem eventSystem;

    public Button button;


    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Exited");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
    }
}
