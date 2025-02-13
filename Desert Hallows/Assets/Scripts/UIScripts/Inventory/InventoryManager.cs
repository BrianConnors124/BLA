using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    public GameObject inventoryMenu;
    private float inventoryCD;
    public GameObject pauseScreen;
    private int currentPause;
    private Action setPauseButton;
    public ItemSlot[] itemSlot;
    public ItemSlot[] abilitySlot;
    public ItemSlot selectedSlot;
    public Action simpleUpdate;
    public InventoryAnimationDone anim;
    public Player player;

    public PickedUpItem notification;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI strText;

    public ItemInfo emptyItem;
    
    

    private void Start()
    {
        inventoryMenu.SetActive(true);
        anim = inventoryMenu.GetComponent<InventoryAnimationDone>();
        notification = GetComponent<PickedUpItem>();
        InputSystemController.instance.openInventory += ToggleInventory;
        InputSystemController.instance.pauseMenu += TogglePauseMenu;
        InputSystemController.instance.selectItem += SelectItem;
        InputSystemController.instance.unselectItem += UnSelectItem;
        InputSystemController.instance.useItem += UseItem;
    }

    private void ToggleInventory()
    {
        if (!pauseScreen.activeInHierarchy && (!anim.inAnimation || !anim.isActiveAndEnabled))
        {
            currentPause %= 2;
            Time.timeScale = currentPause;

            if (currentPause == 0)
            {
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("InventoryNav");
                UpdateStats();
                inventoryMenu.SetActive(true);
            }
            else
            {
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("Movement");
                anim.Disable();
            }
            currentPause++; 
        }
    }
    
    public void TogglePauseMenu()
    {
        if (inventoryMenu.activeInHierarchy)
        {
            ToggleInventory();
        }
        else
        {
            currentPause %= 2;
            Time.timeScale = currentPause;  
            
        
            if (currentPause == 0)
            {
                pauseScreen.SetActive(true);
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("PauseMenuNav");
            }
            else
            {
                pauseScreen.SetActive(false);
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("Movement");
            }
        
            currentPause++;   
        }
    }
    


    public void AddItem(ItemInfo item, int quantity)
    {
        notification.PickedUpNewItem(item, quantity);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].GetItemName().Equals(item.itemName))
            {
                itemSlot[i].AddSlotQuantity(quantity);
                return;
            }
            if (!itemSlot[i].isOccupied)
            {
                print(itemSlot[i].name);
                itemSlot[i].ChangeInfo(item, quantity);
                return;
            }
        }
    }

    private void SelectItem()
    {
        selectedSlot = eventSystem.currentSelectedGameObject?.GetComponent<ItemSlot>();
        
    }

    private void UpdateStats()
    {
        hpText.text = "" + player.maxHealth;
        strText.text = "" + player.damage;
    }
    
    private void UnSelectItem()
    {
        if(eventSystem.currentSelectedGameObject && selectedSlot && selectedSlot != eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>())
            SwitchItems(selectedSlot, eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>());
        
    }

    private void UseItem()
    {
        if (eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().GetItem().isAnAbility)
        {
            HotKeyAbility(eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>());
            return;
        }
        
        eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().UseItem();
        simpleUpdate.Invoke();
        UpdateStats();
    }

    private void SwitchItems(ItemSlot firstSlot, ItemSlot newSlot)
    {
        if (firstSlot.GetItem().itemName.Equals(newSlot.GetItem().itemName))
        {
            newSlot.AddSlotQuantity(firstSlot.GetSlotQuantity());
            firstSlot.ChangeInfo(emptyItem, 0);
        }
        else
        {
            var placeHolderItem = firstSlot.GetItem();
            var placeHolderQuantity = firstSlot.GetSlotQuantity();
            firstSlot.ChangeInfo(newSlot.GetItem(), newSlot.GetSlotQuantity());
            newSlot.ChangeInfo(placeHolderItem, placeHolderQuantity);
        }

        simpleUpdate.Invoke();
    }
    
    
    private void HotKeyAbility(ItemSlot firstSlot)
    {
        ItemSlot newSlot = null;

        for (int i = 0; i < abilitySlot.Length; i++)
        {
            if (abilitySlot[i].isOccupied)
            {
                
            }
            else
            {
                newSlot = abilitySlot[i];
            }
        }

        if (newSlot == null) return;
        
        if (firstSlot.GetItem().itemName.Equals(newSlot.GetItem().itemName))
        {
            newSlot.AddSlotQuantity(firstSlot.GetSlotQuantity());
            firstSlot.ChangeInfo(emptyItem, 0);
        }
        else
        {
            var placeHolderItem = firstSlot.GetItem();
            var placeHolderQuantity = firstSlot.GetSlotQuantity();
            firstSlot.ChangeInfo(newSlot.GetItem(), newSlot.GetSlotQuantity());
            newSlot.ChangeInfo(placeHolderItem, placeHolderQuantity);
        }

        simpleUpdate.Invoke();
    }
}
