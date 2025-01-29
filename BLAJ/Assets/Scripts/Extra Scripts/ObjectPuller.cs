using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ObjectPuller
{

    public static void PullObjectAndSetText(List<GameObject> obj, Vector3 origin, string text)
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
    public static void PullObjectAndSetText(GameObject[] obj, Vector3 origin, string text)
    {
        int currentObj;
        var needNewGameObject = true;
        for (currentObj = 0; currentObj < obj.Length; currentObj++)
        {
            if (!obj[currentObj].activeInHierarchy)
            {
                needNewGameObject = false;
                break;
            }
        }
        
        
        if (needNewGameObject)
        {
            GameObject[] newObj = new GameObject[obj.Length + 1];

            for(int i = 0; i < obj.Length; i++)
            {
                newObj[i] = obj[i];
            }

            newObj[^1] = Object.Instantiate(obj[0]);
            obj = newObj;
            currentObj = obj.Length - 1;
        }
        obj[currentObj].transform.position = origin;
        SetObjectText(text, obj[currentObj].GetComponent<TextMeshPro>());
        obj[currentObj].GetComponent<TextMeshPro>().text = text;
        obj[currentObj].SetActive(true);
    }
    

    public static void PullObject(GameObject[] obj, Vector3 origin)
    {
        int currentObj;
        var needNewGameObject = true;
        for (currentObj = 0; currentObj < obj.Length; currentObj++)
        {
            if (!obj[currentObj].activeInHierarchy)
            {
                needNewGameObject = false;
                break;
            }
        }
        if (needNewGameObject)
        {
            GameObject[] newObj = new GameObject[obj.Length + 1];

            for(int i = 0; i < obj.Length; i++)
            {
                newObj[i] = obj[i];
            }

            newObj[^1] = Object.Instantiate(obj[0]);
            obj = newObj;
            currentObj = obj.Length - 1;
        }
        currentObj--;
        obj[currentObj].transform.position = origin;
        obj[currentObj].SetActive(true);
    }
    
    public static void PullObject(List<GameObject> obj, Vector3 origin)
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
        if (needNewGameObject)
        {
            obj.Add(Object.Instantiate(obj[0]));
        }
        
        obj[^1].transform.position = origin;
        obj[^1].SetActive(true);
    }
    
    public static void PullObject(List<GameObject> obj, Vector3 origin, GameObject other)
    {
        int currentObj;
        var needNewGameObject = true;
        for (currentObj = 0; currentObj < obj.Count; currentObj++)
        {
            if (!obj[currentObj].activeInHierarchy)
            {
                needNewGameObject = false;
                Debug.Log("Hey Niggar");
                break;
            }
        }
        if (needNewGameObject)
        {
            obj.Add(Object.Instantiate(obj[0]));
        }
        
        obj[currentObj].transform.position = origin;
        obj[currentObj].SetActive(true);
        other.SetActive(false);
    }
    
    public static void PullProjectile(List<GameObject> obj, Vector3 origin, Vector2 direction)
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
        
        if (needNewGameObject)
        {
            obj.Add(Object.Instantiate(obj[0]));
            currentObj = obj.Count - 1;
        }
        obj[currentObj].transform.position = origin;
        obj[currentObj].SetActive(true);
        obj[currentObj].GetComponent<Projectile>().SetAim(direction);
    }

    private static void SetObjectText(string text, TextMeshPro obj)
    {
        obj.text = text;
    }

    
    
}
