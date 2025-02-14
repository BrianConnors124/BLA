using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKey : MonoBehaviour
{
    public InputActionReference action;
    

    public GameObject buttonDisplayKeyboard;
    public GameObject buttonDisplayController;
    public GameObject buttonDisplayName;
    public GameObject waitingForButtonDisplay;
    private TextMeshProUGUI buttonTextKeyBoard;
    private TextMeshProUGUI buttonTextController;

    private Player player;
    
    
    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private bool waitingForInput;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        buttonTextKeyBoard = buttonDisplayKeyboard.GetComponent<TextMeshProUGUI>();
        buttonTextController = buttonDisplayController.GetComponent<TextMeshProUGUI>();
    }

    public void RebindKey()
    {
        buttonDisplayKeyboard.SetActive(false);
        buttonDisplayController.SetActive(false);
        waitingForButtonDisplay.SetActive(true);
        buttonDisplayName.SetActive(false);
        

        if (player.usingGamePad)
        {
            _rebindingOperation = action.action.PerformInteractiveRebinding(0)
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(true))
                .Start();
        }
        else
        {
            _rebindingOperation = action.action.PerformInteractiveRebinding(1)
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(false))
                .Start();
        }
        
        
    }


    private void RebindComplete(bool isController)
    {
        int bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
        if (isController)
        {
            buttonTextController.text = InputControlPath.ToHumanReadableString(action.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        else
        {
            buttonTextKeyBoard.text = InputControlPath.ToHumanReadableString(action.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        
        _rebindingOperation.Dispose();
        waitingForButtonDisplay.SetActive(false);
        buttonDisplayName.SetActive(true);
        buttonDisplayKeyboard.SetActive(true);
        buttonDisplayController.SetActive(true);
        
    }
    
    
}
