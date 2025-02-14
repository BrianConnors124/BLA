using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDream : MonoBehaviour
{
    public Vector3 endPos;
    public float speed;
    private bool startAction;
    private RectTransform _transform;

    public void Awake()
    {
        _transform = GetComponent<RectTransform>();
        endPos = _transform.anchoredPosition;
    }

    public void Beamy(Vector3 pos)
    {
        _transform.anchoredPosition = pos;
        startAction = true;
    }

    private void FixedUpdate()
    {
        if (!startAction)
        {
            
        }
        else
        {
            _transform.anchoredPosition += (Vector2) new Vector2((endPos.x - _transform.anchoredPosition.x) * speed / 50 , 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<Player>().ReceiveDamage(10, 0, 0, 0);
        }
    }
}
