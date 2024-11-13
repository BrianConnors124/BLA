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
using Timer = Unity.VisualScripting.Timer;

public class WeaponScript : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private float attackRadius;
    [SerializeField] private float secondaryAttackSize;
    [SerializeField] private bool primaryAttackHasAOE = false;
    private Action primaryAttack;
    private Action secondaryAttack;
    private Rigidbody2D rb;

    private UniversalTimer primaryCD;
    private UniversalTimer secondaryCD;
    private GameObject parent;
    private bool start;

    private Vector2 OBJSCALE;
    private Action reset;
    
    
    // Start is called before the first frame update
    void Start()
    {
        reset += ResetPresets;
        OBJSCALE = transform.localScale;
        start = true;
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
    private void ResetPresets()
    {
        transform.localScale = OBJSCALE;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    
    private void Primary()
    {
        if (primaryCD.TimerDone)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GameObject other;
            StartCoroutine(primaryCD.Timer(.4f));
            if (primaryAttackHasAOE)
            {
                RaycastHit2D[] a = Physics2D.CircleCastAll(transform.position, attackRadius,new Vector2(NegOrPos(transform.position.x), transform.position.y), 0, LayerMask.GetMask("Enemy"));
                for (int i = 0; i < a.Length; i++)
                {
                    print(a[i].collider);
                    Destroy(a[i].collider.gameObject);
                }
            }
            else if(!primaryAttackHasAOE)
            {
                RaycastHit2D a = Physics2D.CircleCast(transform.position, attackRadius,new Vector2(NegOrPos(transform.position.x), transform.position.y), 0, LayerMask.GetMask("Enemy"));
                print(a.collider);
                Destroy(a.collider.gameObject);
            }
        }
        StartCoroutine(new UniversalTimer().Timer(0.2f, reset));
    }
    

    // private void Secondary()
    // {
    //     StartCoroutine(Secondary1());
    // }
    private void Secondary()
    {
        if (secondaryCD.TimerDone && primaryCD.TimerDone)
        {
            print("change scale");
            transform.localScale = new Vector2(OBJSCALE.x * 2, OBJSCALE.y * 2);
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(secondaryCD.Timer(1.5f));
            StartCoroutine(primaryCD.Timer(.5f));
            //RaycastHit2D[] a = Physics2D.CircleCastAll(transform.position, attackRadius,new Vector2(NegOrPos(transform.position.x), transform.position.y), reach, LayerMask.GetMask("Enemy"));
            RaycastHit2D[] a = Physics2D.BoxCastAll(new Vector3((0.5f* secondaryAttackSize) * PlayerController.instance.direction + transform.position.x, transform.position.y), new Vector2(secondaryAttackSize, transform.localScale.y), 0, new Vector2(NegOrPos(transform.position.x), transform.position.y), 0, LayerMask.GetMask("Enemy"));
            print(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                Destroy(a[i].collider.gameObject);
            }
        }
        StartCoroutine(new UniversalTimer().Timer(0.2f, reset));
    }

    private float NegOrPos(float a)
    {
        return a / math.abs(a);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        if(start){
            Gizmos.DrawWireCube(
                new Vector3((0.5f * secondaryAttackSize) * PlayerController.instance.direction + transform.position.x,
                    transform.position.y), new Vector2(secondaryAttackSize, transform.localScale.y));
        }
        else
        {
            Gizmos.DrawWireCube(
                new Vector3((0.5f * secondaryAttackSize) + transform.position.x,
                    transform.position.y), new Vector2(secondaryAttackSize, transform.localScale.y));
        }
    }
}
