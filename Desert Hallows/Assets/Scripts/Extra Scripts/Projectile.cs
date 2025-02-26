using System;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float damage;
    public float speed;
    public Enemy enemy;

    public Vector2 aim;
    private bool aimEnabled;
    private bool startTimer;
    private float timer;

    private bool canHitPlayer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Initialize(Vector2 a, Enemy enemy)
    {
        var mult = 1;
        if (a.x < 0) mult = -1;
        aim = a;
        transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * mult, transform.localScale.y);
        this.enemy = enemy;
        Begin();
    } 

    private void OnEnable()
    {
        timer = 1;
        startTimer = true;
        canHitPlayer = true;
    }

    private void OnDisable()
    {
        startTimer = false;
    }

    private void Begin()
    {
        rb.velocity = Direction(transform.position,aim) * speed;
        var mult = 1;
        if (rb.velocity.x < 0) mult = -1;
        transform.localScale = new Vector2(Math.Abs(transform.localScale.x) * mult, transform.localScale.y);
    }
    
    private Vector2 Direction(Vector2 a, Vector2 b) => (b - a).normalized;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canHitPlayer)
        {
            other.GetComponent<Player>().ReceiveDamage(damage, enemy.knockBack, 0, enemy.PlayerDirection());
            canHitPlayer = false;
        }
    }

    private void Update()
    {
        if (startTimer) timer -= Time.deltaTime;
        if(timer <= 0) gameObject.SetActive(false);
    }
}
