using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;

    public EnemyAnimator instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void UpdateAnimator(bool walking, bool takingDamage)
    {
        animator.SetBool("Walking", walking);
        animator.SetBool("TakingDamage", takingDamage);
    }
}
