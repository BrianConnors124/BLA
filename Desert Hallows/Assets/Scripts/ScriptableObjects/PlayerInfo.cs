using UnityEngine;

[CreateAssetMenu(menuName = "Players/NewPlayer", fileName = "NewPlayer")]
public class PlayerInfo : ScriptableObject
{
    public float movementSpeed = 8;
    public float dashSpeed = 64;
    public float jumpHeight = 24;
    public float attackCD = .5f;
    public float superAttackCD = 20;
    public float dashCD = 1;
    public float dashDuration = 0.08f;
    public float doubleJumps = 1;
    public float coyoteJump = 0.2f;
    public float gravityScale = 10;
    public float baseDamage = 10;
    public float baseKnockBack = 12;
    public float stun = 0.5f;
    public float health = 100;
}