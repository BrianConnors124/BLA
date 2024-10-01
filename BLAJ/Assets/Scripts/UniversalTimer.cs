using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UniversalTimer
{
    public float timer = 0;
    private float timerLength;
    
    int firstTimeUse;

    public bool TimerDone => timer.Equals(0);


    public void Reset()
    {
        timer = 0;
    }
    public IEnumerator Timer(float Length, int Repeat, Action Commit)
    {
        for (int i = 0; i < Repeat; i++)
        {
            timer = Length; 
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, Length);
                yield return new WaitForEndOfFrame();
            }
            if(timer <= 0)
                Commit.Invoke();
        }
    }
    public IEnumerator Timer(float Length, int Repeat)
    {
        for (int i = 0; i < Repeat; i++)
        {
            timer = Length; 
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                timer = Mathf.Clamp(timer, 0, Length);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public IEnumerator Timer(float Length, Action Commit)
    {
        timer = Length; 
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, Length);
            yield return new WaitForEndOfFrame();
        }
        if(timer <= 0)
            Commit.Invoke();
    }
    
    public IEnumerator Timer(float Length)
    {
        timer = Length; 
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, Length);
            yield return new WaitForEndOfFrame();
        }
    }

 
}
