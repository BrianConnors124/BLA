using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private int currentPause;
    private Action setPauseButton;

    private void Start()
    {
        InputSystemController.instance.pauseGame += PauseGame;
        inventoryMenu.SetActive(false);
        
    }

    private void PauseGame()
    {
        currentPause %= 2;
        Time.timeScale = currentPause;

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

    public void AddItem(string itemName, int quantity, Sprite image)
    {
        Debug.Log(itemName + " " + quantity);
    }
}
