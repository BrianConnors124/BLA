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
    public GameObject pauseScreen;
    private int currentPause;
    private Action setPauseButton;
    public ItemSlot[] itemSlot;
    public ItemSlot selectedSlot;
    public GameObject description;

    public ItemInfo emptyItem;
    

    private void Start()
    {
        inventoryMenu.SetActive(true);
        InputSystemController.instance.openInventory += OpenInventory;
        InputSystemController.instance.pauseMenu += OpenPauseMenu;
        InputSystemController.instance.selectItem += SelectItem;
        InputSystemController.instance.unselectItem += UnSelectItem;
    }

    private void OpenInventory()
    {
        currentPause %= 2;
        Time.timeScale = currentPause;
        InputSystemController.instance.playerInput.SwitchCurrentActionMap(InputSystemController.instance.actionMaps[currentPause]);

        if (currentPause == 0)
        {
            inventoryMenu.SetActive(true);
        }
        else
        {
            inventoryMenu.GetComponent<AnimationDone>().Disable();
        }
        currentPause++;
    }
    
    private void OpenPauseMenu()
    {
        currentPause %= 2;
        Time.timeScale = currentPause;
        InputSystemController.instance.playerInput.SwitchCurrentActionMap(InputSystemController.instance.actionMaps[currentPause]);

        if (currentPause == 0)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
        currentPause++;
    }

    private void FixedUpdate()
    {
        if(eventSystem.currentSelectedGameObject != null && inventoryMenu.activeInHierarchy && eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().slotQuantity > 0) description.GetComponent<ShowDescription>().UpdateDescription(eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().currentItem);
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
    }
}
