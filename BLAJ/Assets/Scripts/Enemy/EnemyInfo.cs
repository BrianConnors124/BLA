using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string name;
    public string description;
    public float health;
    public float jumpHeight;
    public float movementSpeed;
    public float damage;
}