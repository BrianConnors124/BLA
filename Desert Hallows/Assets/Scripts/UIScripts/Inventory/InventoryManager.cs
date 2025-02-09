using System;
using System.Collections;
using System.Collections.Generic;
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
    public ItemSlot selectedSlot;
    public Action simpleUpdate;
    public InventoryAnimationDone anim;

    public ItemInfo emptyItem;
    
    

    private void Start()
    {
        inventoryMenu.SetActive(true);
        anim = inventoryMenu.GetComponent<InventoryAnimationDone>();
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
    
    private void TogglePauseMenu()
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
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("PauseMenuNav");
                pauseScreen.SetActive(true);
            }
            else
            {
                InputSystemController.instance.playerInput.SwitchCurrentActionMap("Movement");
                pauseScreen.SetActive(false);
            }
        
            currentPause++;   
        }
    }
    
    


    public void AddItem(ItemInfo item, int quantity)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isOccupied || itemSlot[i].currentItem.itemName.Equals(item.itemName))
            {
                itemSlot[i].AddItem(item, quantity);
                return;
            }
        }
    }

    private void SelectItem()
    {
        selectedSlot = eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>();
        
    }
    private void UnSelectItem()
    {
        if(selectedSlot != eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>())
            SwitchItems(selectedSlot, eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>());
        
    }

    private void UseItem()
    {
        eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().UseItem();
        simpleUpdate.Invoke();
    }

    private void SwitchItems(ItemSlot firstSlot, ItemSlot newSlot)
    {
        if (firstSlot.currentItem.itemName.Equals(newSlot.currentItem.itemName))
        {
            newSlot.UpdateInfo(newSlot.slotQuantity += firstSlot.slotQuantity);
            
            firstSlot.currentItem = emptyItem;
            firstSlot.UpdateInfo(0);
        }
        else
        {
            var placeHolder = ScriptableObject.CreateInstance<ItemInfo>();

            placeHolder = firstSlot.currentItem;
            
            firstSlot.currentItem = newSlot.currentItem;
            newSlot.currentItem = placeHolder;

            var placeHolderInt = firstSlot.slotQuantity;
            firstSlot.UpdateInfo(newSlot.slotQuantity);
            newSlot.UpdateInfo(placeHolderInt);   
        }

        simpleUpdate.Invoke();
    }
}
