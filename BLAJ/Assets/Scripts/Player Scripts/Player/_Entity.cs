using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _anim;

    [SerializeField] protected float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    public Animator Anim => _anim;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetAnimState(string state ,bool value)
    {
        _anim.SetBool(state, value);
    }

    public void ZeroVelocity() => _rb.velocity = Vector2.zero;
    public void SetVelocity(Vector2 newVelocity) => _rb.velocity = newVelocity;
    public Vector2 Velocity => _rb.velocity;
}
