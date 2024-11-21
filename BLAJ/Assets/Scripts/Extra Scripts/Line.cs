using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class Line
{
    public static void Draw(
        Vector2 origin, 
        Vector2 direction,
        float distance
    )
    {
        float lenX = direction.x;
        float lenY = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(lenX,2) + Math.Pow(lenY, 2));
        float conversionRate = distance / hyp;
        float newX = lenX * conversionRate;
        float newY = lenY * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint);
    }
    
    public static void Draw(
        Vector2 origin, 
        Vector2 direction,
        float distance,
        Color color
    )
    {
        float lenX = direction.x;
        float lenY = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(lenX,2) + Math.Pow(lenY, 2));
        float conversionRate = distance / hyp;
        float newX = lenX * conversionRate;
        float newY = lenY * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint, color);
    }
    public static RaycastHit2D Create(
        Vector2 origin, 
        Vector2 direction,
        float distance
    )
    {
        return Physics2D.Raycast(origin, direction, distance);
    }
    public static RaycastHit2D Create(
        Vector2 origin, 
        Vector2 direction,
        float distance,
        LayerMask layerMask
    )
    {
        return Physics2D.Raycast(origin, direction, distance,layerMask);
    }
    
    public static RaycastHit2D CreateAndDraw(
        Vector2 origin, 
        Vector2 direction,
        float distance
    )
    {
        float x = 0;
        float y = 0;
        if (direction.x != 0)
        {
            x = direction.x - origin.x;
        }
        if (direction.y != 0)
        {
            y = direction.y - origin.y;
        }
        float hyp = (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y, 2));
        float conversionRate = distance / hyp;
        float newX = x * conversionRate;
        float newY = y * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint);
        return Physics2D.Raycast(origin, direction, distance);
    }
    
    public static RaycastHit2D CreateAndDraw(
        Vector2 origin, 
        Vector2 direction,
        float distance, 
        LayerMask layerMask
    )
    {
        float lenX = direction.x;
        float lenY = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(lenX,2) + Math.Pow(lenY, 2));
        float conversionRate = distance / hyp;
        float newX = lenX * conversionRate;
        float newY = lenY * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint);
        return Physics2D.Raycast(origin, direction, distance, layerMask);
    }
    public static RaycastHit2D CreateAndDraw(
        Vector2 origin, 
        Vector2 direction,
        float distance, 
        Color color
    )
    {
        float lenX = direction.x;
        float lenY = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(lenX,2) + Math.Pow(lenY, 2));
        float conversionRate = distance / hyp;
        float newX = lenX * conversionRate;
        float newY = lenY * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint, color);
        return Physics2D.Raycast(origin, direction, distance);
    }
    
    public static RaycastHit2D CreateAndDraw(
        Vector2 origin, 
        Vector2 direction,
        float distance, 
        LayerMask layerMask,
        Color color
    )
    {
        float lenX = direction.x;
        float lenY = direction.y;
        float hyp = (float)Math.Sqrt(Math.Pow(lenX,2) + Math.Pow(lenY, 2));
        float conversionRate = distance / hyp;
        float newX = lenX * conversionRate;
        float newY = lenY * conversionRate;
        Vector2 endPoint = new Vector2(newX, newY);
        Debug.DrawLine(origin,origin + endPoint, color);
        return Physics2D.Raycast(origin, direction, distance, layerMask);
    }

    public static float Length(Vector2 origin, Vector2 endPoint)
    {
        return (float)Math.Sqrt((Math.Pow(endPoint.x - origin.x, 2) + Math.Pow(endPoint.y - origin.y, 2)));
    }
    
    public static float Length(Vector3 origin, Vector3 endPoint)
    {
        return (float)Math.Sqrt((Math.Pow(endPoint.x - origin.x, 2) + Math.Pow(endPoint.y - origin.y, 2)) + Math.Pow(endPoint.z - origin.z, 2));
    }
}
