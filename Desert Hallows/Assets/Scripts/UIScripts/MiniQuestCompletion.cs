using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniQuestCompletion : MonoBehaviour
{
    public GameObject[] enableOnCompletion;
    public GameObject[] disableOnCompletion;
    

    public void Completed(int numba)
    {
        
        if(enableOnCompletion[numba] && disableOnCompletion[numba].activeInHierarchy) enableOnCompletion[numba].SetActive(true);
        if(disableOnCompletion[numba]&& disableOnCompletion[numba].activeInHierarchy) Destroy(disableOnCompletion[numba]);
    }
}
