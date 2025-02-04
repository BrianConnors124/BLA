using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed
{
    private float speedNum;
    private float speedAccel;
    private float speedMax;
    private float speedMin;

    public static float Calculator(float speed, float accel,float max)
    {
        speed += accel;
        speed = Mathf.Clamp(speed, -max, max);
        return speed;
    }
    
    
}
