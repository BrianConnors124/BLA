using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    public Image healthShadeSlider;
    public Image healthSlider;
    public Image shieldSlider;
    public float maxHealth;
    public float maxShield;
    public float currentShield;
    private SheildScript _shield;
    private Enemy _enemy;
    public Transform _enemyTransform;
    private RectTransform _transform;
    

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _enemy = GetComponentInParent<Enemy>();
        maxHealth = _enemy.info.health;
        _enemy.onTakeDamage += UpdateHealthSlider;
        if (_enemy.sheild)
        {
            _shield = _enemy.sheild.GetComponent<SheildScript>();
            maxShield = _shield.hitPoints;
            _enemy.onTakeDamage += UpdateShieldSlider;
        }
        else shieldSlider.fillAmount = 0;
    }

    private void Update()
    {
        Vector2 scale = _transform.localScale;
        _transform.localScale = new Vector2(Mathf.Abs(scale.x) * _enemy.MovementDirection(), scale.y);
    }

    private void UpdateHealthSlider()
    {
        healthSlider.fillAmount = maxHealth;
        healthShadeSlider.fillAmount = (maxHealth - _enemy.health) / maxHealth;
        if(_enemy.health == 0) Destroy(gameObject);
    }

    private void UpdateShieldSlider()
    {
        shieldSlider.fillAmount = _shield.hitPoints / maxShield;
        if(_enemy.health == 0) Destroy(gameObject);
    }
}
