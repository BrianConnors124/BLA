using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Line
{
    public static void Draw(
        Vector2 origin, 
        Vector2 direction,
        float distance
    )
    {
        float x = direction.x;
        float y = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y, 2));
        float conversionRate = distance / hyp;
        float newX = x * conversionRate;
        float newY = y * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,endPoint);
    }
    
    public static void Draw(
        Vector2 origin, 
        Vector2 direction,
        float distance,
        Color color
    )
    {
        float x = direction.x;
        float y = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y, 2));
        float conversionRate = distance / hyp;
        float newX = x * conversionRate;
        float newY = y * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint, color);
    }
}
