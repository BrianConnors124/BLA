using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public ProjectileInfo info;
    private Rigidbody2D rb;
    public float damage;
    public float speed;

    public Vector2 aim;
    
    private void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = info.anim;
        rb = GetComponent<Rigidbody2D>();
        damage = info.damage;
        speed = info.speed;
    }

    public void SetAim(Vector2 a) => aim = a;

    private void OnEnable()
    {
        Begin();
    }

    private void Begin()
    {
        rb.velocity = Direction(transform.position, aim).normalized * speed;
    }

    private Vector2 Direction(Vector2 a, Vector2 b)
    {
        return a - b;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))other.collider.GetComponent<Player>().ReceiveDamage(damage, 0, 0, 0);
    }
}



[CreateAssetMenu(menuName = "Projectile/New Projectile")]
public class ProjectileInfo : ScriptableObject
{
    public AnimatorController anim;
    public float damage;
    public float speed;
}
