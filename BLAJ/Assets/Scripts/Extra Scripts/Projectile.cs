using UnityEngine;


public class Projectile : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float damage;
    public float speed;

    public Vector2 aim;
    private bool aimEnabled;
    private bool startTimer;
    private float timer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetAim(Vector2 a)
    {
        aim = a;
        Begin();
    } 

    private void OnEnable()
    {
        timer = .01f;
    }

    private void OnDisable()
    {
        startTimer = false;
    }

    private void Begin()
    {
        rb.velocity = Direction(aim,transform.position).normalized * speed;
    }

    private Vector2 Direction(Vector2 a, Vector2 b)
    {
        return a - b;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))other.collider.GetComponent<Player>().ReceiveDamage(damage, 0, 0, 0);
        startTimer = true;
    }

    private void Update()
    {
        if (startTimer) timer -= Time.deltaTime;
        if(timer <= 0) gameObject.SetActive(false);
    }
}
