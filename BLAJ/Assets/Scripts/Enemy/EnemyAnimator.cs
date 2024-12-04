using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void UpdateAnimator(bool walking, bool takingDamage, bool attacking)
    {
        animator.SetBool("Walking", walking);
        animator.SetBool("TakingDamage", takingDamage);
        animator.SetBool("Attacking", attacking);
    }
}
