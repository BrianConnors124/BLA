using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKey : MonoBehaviour
{
    public InputActionReference action;
    

    public GameObject buttonDisplay;
    public GameObject buttonDisplayName;
    public GameObject waitingForButtonDisplay;
    private TextMeshProUGUI buttonText;

    public bool isControllerBinding;
    
    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private bool waitingForInput;


    private void Start()
    {
        buttonText = buttonDisplay.GetComponent<TextMeshProUGUI>();
    }

    public void RebindKey()
    {
        buttonDisplay.SetActive(false);
        waitingForButtonDisplay.SetActive(true);
        buttonDisplayName.SetActive(false);

        if (isControllerBinding)
        {
            _rebindingOperation = action.action.PerformInteractiveRebinding(0)
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete())
                .Start();
        }
        else
        {
            _rebindingOperation = action.action.PerformInteractiveRebinding(1)
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete())
                .Start();
        }
        
        
    }


    private void RebindComplete()
    {
        int bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
        buttonText.text = InputControlPath.ToHumanReadableString(action.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _rebindingOperation.Dispose();
        waitingForButtonDisplay.SetActive(false);
        buttonDisplayName.SetActive(true);
        buttonDisplay.SetActive(true);
    }
    
    
}
