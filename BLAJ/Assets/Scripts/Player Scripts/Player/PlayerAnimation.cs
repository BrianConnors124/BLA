using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    public void UpdateAnimator(bool walkingRight,bool walkingLeft, bool takingDamage, bool attacking, bool jumping)
    {
        animator.SetBool("WalkingRight", walkingRight);
        animator.SetBool("WalkingLeft", walkingLeft);
        animator.SetBool("TakingDamage", takingDamage);
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Jumping", jumping);
    }
}
