using System;
using System.Collections.Generic;
using UnityEngine;

public class UniversalTimer : MonoBehaviour
{
    #region Variables
    
    public List<string> key;
    private Dictionary<string, float> timer;
    private Dictionary<string, float> startVal;
    private Dictionary<string, Action> action;
    private float timerLength;

    #endregion
    

    public void Start()
    {
        key = new List<string>();
        timer = new Dictionary<string, float>();
        startVal = new Dictionary<string, float>();
        action = new Dictionary<string, Action>();
    }
    
    private void FixedUpdate()
    {
        for (int i = 0; i < timer.Count; i++)
        { timer[key[i]] -= Time.deltaTime;
            if(timer[key[i]] <= 0){
                if(action.ContainsKey(key[i]))
                {
                    action[key[i]].Invoke();
                    action.Remove(key[i]);
                }  
                timer.Remove(key[i]);
                startVal.Remove(key[i]);
                key.RemoveAt(i);
                i--;                   
            }    
        }   
    }
    public void SetActionTimer(string code, float length, Action commit)
    {
        if (!key.Contains(code)) key.Add(code);
        action.TryAdd(code, commit);
        timer.TryAdd(code, length);
        startVal.TryAdd(code, length);
        timer[code] = length;
    }
    
    public void SetTimer(string code, float length)
    {
        if(!key.Contains(code)) key.Add(code);
        timer.TryAdd(code, length);
        startVal.TryAdd(code, length);
        timer[code] = length;
    }

    public float GetTimerValue(string code) => timer[code];
    public bool TimerDone(string a) => !key.Contains(a);

    public bool TimerActive(string code) => key.Contains(code);
    
    public float GetMaxValue(string code) => startVal[code];

    public void RemoveTimer(string code)
    {
        timer.Remove(code);
        startVal.Remove(code);
        key.Remove(code);
        action.Remove(code);
    }

}
