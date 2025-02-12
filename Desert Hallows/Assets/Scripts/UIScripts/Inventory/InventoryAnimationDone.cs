using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryAnimationDone : MonoBehaviour
{
    public GameObject[] setActiveOnAnimDone;
    public EventSystem eventSystem;
    public Animator anim;

    

    public InventoryManager manager;
    
    public bool inAnimation;
    private int a;

    private void Start()
    {
        anim = GetComponent<Animator>();
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        inAnimation = true;
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        
    }
    

    
    public void Disable()
    {
        if (!inAnimation)
        {
            foreach (var a in setActiveOnAnimDone)
            {
                a.SetActive(false);
            }
        
            anim.Play("InventoryClose");   
        }
    }
    
    

    private void OnAnimFinishedOpen()
    {
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(true);
        }

        inAnimation = false;
        manager.simpleUpdate.Invoke();
    }
    
    
    public void AnimFinishedClose()
    {
        gameObject.SetActive(false);
    }
}