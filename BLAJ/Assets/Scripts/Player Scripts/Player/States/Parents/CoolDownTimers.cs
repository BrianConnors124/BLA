using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTimers : MonoBehaviour
{
    public float baseTimer;

    // Update is called once per frame
    public virtual void Update()
    {
        baseTimer -= Time.deltaTime;
    }
}
