using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPuller
{

    public void PullObjectAndSetText(List<GameObject> obj, Vector3 origin, string text)
    {
        int currentObj;
        var needNewGameObject = true;
        for (currentObj = 0; currentObj < obj.Count; currentObj++)
        {
            if (!obj[currentObj].activeInHierarchy)
            {
                needNewGameObject = false;
                break;
            }
        }
        
        //Console.WriteLine(needNewGameObject);
        if (needNewGameObject)
        {
            obj.Add(Object.Instantiate(obj[0]));
            currentObj = obj.Count - 1;
        }
        obj[currentObj].transform.position = origin;
        SetObjectText(text, obj[currentObj].GetComponent<TextMeshPro>());
        obj[currentObj].GetComponent<TextMeshPro>().text = text;
        obj[currentObj].SetActive(true);
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

    private void SetObjectText(string text, TextMeshPro obj)
    {
        obj.text = text;
    }

    
    
}
