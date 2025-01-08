using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed
{
    private float speedNum;
    private float speedAccel;
    private float speedMax;
    private float speedMin;

    public static float CalculatorX(float speed, float accel,float max)
    {
        speed += accel;
        speed = Mathf.Clamp(speed, -max, max);
        return speed;
    }
    
    public static float CalculatorY(float speed, float accel,float max)
    {
        speed += accel;
        speed = Mathf.Clamp(speed, -max, max);
        return speed;
    }
}
