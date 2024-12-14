using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string name;
    public string description;
    public float health;
    public float jumpHeight;
    public float movementSpeed;
    public float damage;
    public float baseReach;
    public float stun;
    public float knockBack;
    public float primaryCD;
    public bool bossType;
}
