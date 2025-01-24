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
    private int currentPause;
    private Action setPauseButton;
    public ItemSlot[] itemSlot;
    public ItemSlot selectedSlot;
    public GameObject description;
    public bool slotSelected;
    

    private void Start()
    {
        InputSystemController.instance.pauseGame += PauseGame;
        InputSystemController.instance.selectItem += SelectItem;
        InputSystemController.instance.unselectItem += UnSelectItem;
    }

    private void PauseGame()
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

    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().currentItem.quantity > 0) description.GetComponent<ShowDescription>().UpdateDescription(eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().currentItem);
    }


    public void AddItem(ItemInfo item)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && (!itemSlot[i].isOccupied || itemSlot[i].currentItem.itemName.Equals(item.itemName)))
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }
    }

    private void SelectItem()
    {
        selectedSlot = eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>();
        slotSelected = true;
    }
    private void UnSelectItem()
    {
        slotSelected = false;
        var placeHolder = selectedSlot.currentItem;
        selectedSlot.currentItem = eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().currentItem;
        selectedSlot.UpdateInfo(eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>().slotQuantity);
        selectedSlot = eventSystem.currentSelectedGameObject.GetComponent<ItemSlot>();
        selectedSlot.currentItem = placeHolder.currentItem;
        selectedSlot.UpdateInfo(placeHolder.slotQuantity);
    }
}
