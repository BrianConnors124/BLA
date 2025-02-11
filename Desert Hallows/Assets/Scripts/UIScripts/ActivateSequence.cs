using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateSequence : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))Activate();
    }

    public void Activate()
    {
        if (activate.Equals(deactivate))
        {
            GameObject.Find("Canvas").GetComponent<InventoryManager>().TogglePauseMenu();
        }
        else
        {
            if(activate)activate.SetActive(true);
            if(deactivate)deactivate.SetActive(false);
            if(gameObject.CompareTag("Background"))Destroy(gameObject);   
        }
    }
}
