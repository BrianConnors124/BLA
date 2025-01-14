using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPuller
{

    public void PullObjectAndSetText(List<GameObject> arg, Vector3 origin, string text)
    {
        int currentPos;
        var needNewGameObject = true;
        for (currentPos = 0; currentPos < arg.Count; currentPos++)
        {
            if (!arg[currentPos].activeInHierarchy)
            {
                needNewGameObject = false;
            }
            else
            {
                break;
            }
        }
        if (needNewGameObject)
        {
            arg.Add(arg[0]);
        }
        currentPos--;
        arg[currentPos].transform.position = origin;
        arg[currentPos].GetComponent<TextMeshPro>().text = text;
        arg[currentPos].SetActive(true);
    }
    
    public void PullObject(List<GameObject> arg, Vector3 origin)
    {
        int currentPos;
        var needNewGameObject = true;
        for (currentPos = 0; currentPos < arg.Count; currentPos++)
        {
            if (!arg[currentPos].activeInHierarchy)
            {
                needNewGameObject = false;
            }
            else
            {
                break;
            }
        }
        if (needNewGameObject)
        {
            arg.Add(arg[0]);
        }
        currentPos--;
        arg[currentPos].transform.position = origin;
        arg[currentPos].SetActive(true);
    }

    private void SetObjectText(string text, GameObject obj)
    {
        
    }

    
    
}
