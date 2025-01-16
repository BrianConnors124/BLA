using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public static class AddToStaticList
{
    static AddToStaticList()
    {
        
    }

    public static void Add(List<GameObject> list, GameObject newObject)
    {
        list.Add(newObject);
    }
}
