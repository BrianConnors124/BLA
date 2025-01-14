using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageNumber : MonoBehaviour
{

    public TextMeshPro damage;
    public Rigidbody2D rb;
    private float time;
    private float size;
    private Color color;
    private float randomNum;

    private void Awake()
    {
        size = damage.fontSize;
        color = damage.color;
    }

    private void OnEnable()
    {
        damage.color = color;
        randomNum = Random.Range(0f, 100f);
        var negOrPos = 1;
        if (randomNum <= 50) negOrPos = -1;
        print(negOrPos);
        damage.fontSize = size;
        rb.velocity = new Vector2(2 * negOrPos, 6);
        time = 0.6f;
    }

    private void Update()
    {
        damage.color = Color.Lerp(damage.color, Color.red, Time.deltaTime * 5);
        time -= Time.deltaTime;
        damage.fontSize += Time.deltaTime * 3;
        if(time <= 0) gameObject.SetActive(false);
    }
}
