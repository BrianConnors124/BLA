using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    public Image healthSlider;
    public float maxHealth;
    private Enemy enemy;
    private Transform enemyTransform;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyTransform = GetComponentInParent<Transform>();
        maxHealth = enemy.info.health;
        enemy.onTakeDamage += UpdateHealthSlider;
    }

    private void Update()
    {
        // if (enemyTransform.localScale.x < 0)
        //     transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * -1, transform.localScale.y);
        // else transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
    }

    private void UpdateHealthSlider()
    {
        healthSlider.fillAmount = (maxHealth - enemy.health) / maxHealth;
        if(enemy.health == 0) Destroy(gameObject);
    }
}
