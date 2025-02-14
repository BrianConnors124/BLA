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

    private float timer = 0;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timer <= 0)
            {
                other.GetComponent<Player>().ReceiveDamage(10, 0, 0, 0);
                timer = .3f;
            }

            timer -= Time.deltaTime;
        }
    }
}
