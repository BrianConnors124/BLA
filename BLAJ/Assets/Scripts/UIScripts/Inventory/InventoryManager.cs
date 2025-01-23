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
            inventoryMenu.SetActive(false);
        }
        currentPause++;
    }
    
    

    public void AddItem(string itemName, int quantity, Sprite itemImage)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemImage);
                return;
            }
        }
    }
}
