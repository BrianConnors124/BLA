using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorialSequence : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))ActivateTutorial();
    }

    public void ActivateTutorial()
    {
        if(activate)activate.SetActive(true);
        if(deactivate)deactivate.SetActive(false);
        if(gameObject.CompareTag("Background"))Destroy(gameObject);
    }
}
