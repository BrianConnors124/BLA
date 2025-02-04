using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationDone : MonoBehaviour
{
    public GameObject[] setActiveOnAnimDone;
    public EventSystem eventSystem;
    public Animator anim;
    public bool animDone;
    private bool disabled;

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
        StopCoroutine(OnAnimFinishedClose());
        disabled = false;
        animDone = false;
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        StartCoroutine(OnAnimFinishedOpen());
    }
    

    public void Disable()
    {
        StopCoroutine(OnAnimFinishedOpen());
        animDone = false;
        anim.Play("InventoryClose");
        if(!disabled) StartCoroutine(OnAnimFinishedClose());
        disabled = true;
    }

    private IEnumerator OnAnimFinishedOpen()
    {
        yield return new WaitUntil(() => animDone);
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(true);
        }
    }
    
    private IEnumerator OnAnimFinishedClose()
    {
        foreach (var a in setActiveOnAnimDone)
        {
            a.SetActive(false);
        }
        yield return new WaitUntil(() => animDone);
        gameObject.SetActive(false);
    }

    public void AnimFinished()
    {
        animDone = true;
    }
}