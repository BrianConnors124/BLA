using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    
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
        if(selectedSlot != null && selectedSlot.currentItem.quantity > 0) description.GetComponent<ShowDescription>().UpdateDescription(selectedSlot.currentItem);
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
}
