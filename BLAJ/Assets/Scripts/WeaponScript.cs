using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class WeaponScript : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private float attackRadius;
    [SerializeField]private float reach;
    [SerializeField] private bool primaryAttackHasAOE;
    private Action primaryAttack;
    private Action secondaryAttack;
    private Rigidbody2D rb;

    private UniversalTimer primaryCD;
    private UniversalTimer secondaryCD;
    private GameObject parent;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Player");
        InputSystemController.instance.primaryAction += Primary;
        InputSystemController.instance.secondaryAction += Secondary;
        primaryCD = new UniversalTimer();
        primaryCD.Reset();
        secondaryCD = new UniversalTimer();
        secondaryCD.Reset();
    }

    

    // Primary/Secondary Attack ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    //public static RaycastHit2D CircleCast()
    //RaycastHit2D CircleCastAll for AOE attacks
    //RaycastHit2D CircleCast for single attacks
    private void Primary()
    {
        if (primaryCD.TimerDone)
        {
            StartCoroutine(primaryCD.Timer(.5f));
            RaycastHit2D a = Physics2D.CircleCast(transform.position, attackRadius,new Vector2(NegOrPos(transform.position.x), transform.position.y), reach, LayerMask.GetMask("Enemy"));
            print(a.transform.position);
            print(GetComponentInParent<Transform>().position);
            
        }
    }

    private void Secondary()
    {
        if (secondaryCD.TimerDone)
        {
            StartCoroutine(secondaryCD.Timer(1.5f));
            RaycastHit2D[] a = Physics2D.CircleCastAll(transform.position, attackRadius,new Vector2(NegOrPos(transform.position.x), transform.position.y), reach, LayerMask.GetMask("Enemy"));
            print(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                print(a[i].transform.position);
            } 
        }
    }

    private float NegOrPos(float a)
    {
        return a / math.abs(a);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
