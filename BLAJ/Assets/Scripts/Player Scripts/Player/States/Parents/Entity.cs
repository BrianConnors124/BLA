using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _anim;
    protected Vector2 hitBox;

    [SerializeField] protected float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    public Animator Anim => _anim;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        hitBox = GetComponent<BoxCollider2D>().size;
        hitBox *= transform.localScale;
    }

    public void SetAnimState(string state ,bool value)
    {
        _anim.SetBool(state, value);
    }

    public void ZeroVelocity() => _rb.velocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => _rb.velocity = newVelocity;
    public Vector2 Velocity => _rb.velocity;
    public float Direction(float a) => Mathf.Abs(a) / a;
    public bool Flip(Rigidbody2D rb, bool currentFlip)
    {
        if (rb.velocityX > 0)
            return false;
        if (rb.velocityX < 0)
            return true;
        return currentFlip;
    }

    public bool IsTouchingGround() => BoxCastDrawer.BoxCastAndDraw(new Vector2(transform.position.x, transform.position.y - hitBox.y * 0.47f), new Vector2(hitBox.x, 0.2f), 0,
        Vector2.down, 0, LayerMask.GetMask("WorldObj"));
}
