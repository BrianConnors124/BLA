using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBounce : MonoBehaviour
{
    [Range(0, 100)] public float speed;
    private RectTransform newTransform;
    private float originalPos;
    private Vector2 Pos => newTransform.position;
    private bool flip;

    private void Start()
    {
        newTransform = GetComponent<RectTransform>();
        originalPos = newTransform.position.x;
    }

    void LateUpdate()
    {
        if(!flip)newTransform.position = new Vector2(Pos.x + speed/100, Pos.y);
        else newTransform.position = new Vector2(Pos.x - speed/100, Pos.y);

        if (!flip)flip = Pos.x > 10 + originalPos;
        else flip = Pos.x > originalPos - 10;
    }
}
