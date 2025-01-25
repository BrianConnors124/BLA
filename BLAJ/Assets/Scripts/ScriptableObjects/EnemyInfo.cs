using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/New Enemy", fileName = "New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string named;
    public AnimatorController anim;
    public string description;
    public float health = 100;
    public float jumpHeight = 12;
    public float movementSpeed = 6;
    public float damage = 10;
    public float baseReach = 1;
    public float stun = 0;
    public float knockBack = 10;
    public float primaryCD = 1;
    public bool bossType;
}
