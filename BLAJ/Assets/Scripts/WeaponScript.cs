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
using UnityEngine.XR;
using Timer = Unity.VisualScripting.Timer;

public class WeaponScript : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private float attackRadius;
    [SerializeField] private float secondaryAttackSize;
    private Action primaryAttack;
    private Action secondaryAttack;
    private Rigidbody2D rb;

    private UniversalTimer primaryCD;
    private UniversalTimer secondaryCD;
    private bool start;

    private Vector2 OBJSCALE;
    private Action reset;
    private RaycastHit2D[] a;
    
    
    
    void Start()
    {
        reset += ResetPresets;
        OBJSCALE = transform.localScale;
        start = true;
        InputSystemController.instance.primaryAction += Primary;
        InputSystemController.instance.secondaryAction += Secondary;
        primaryCD = new UniversalTimer();
        primaryCD.Reset();
        secondaryCD = new UniversalTimer();
        secondaryCD.Reset();
    }
    private void Update()
    {
        Vector2 a = new Vector2(HandMovement.instance.dir2.y, HandMovement.instance.dir2.x * -1);
        if (HandMovement.instance.armMovesWithMovement)
        {
            a = new Vector2(HandMovement.instance.dir.y, HandMovement.instance.dir.x * -1);
        }
        BoxCastDrawer.BoxCastAndDraw(transform.position, transform.localScale * secondaryAttackSize, 0, a, 15);  
        
    }

    
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
            StartCoroutine(primaryCD.Timer(.4f));
            RaycastHit2D[] a = Physics2D.CircleCastAll(transform.position, attackRadius,transform.position * PlayerController.instance.direction, 0, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < a.Length; i++)
            {
             print(a[i].collider);
             Destroy(a[i].collider.gameObject);
            }
            StartCoroutine(new UniversalTimer().Timer(0.2f, reset));   
        }
    }
    
    
    private void Secondary()
    {
        if (secondaryCD.TimerDone && primaryCD.TimerDone)
        {
            print("change scale");
            transform.localScale = new Vector2(OBJSCALE.x * 2, OBJSCALE.y * 2);
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(secondaryCD.Timer(1.5f));
            StartCoroutine(primaryCD.Timer(.5f));
            a = Physics2D.BoxCastAll(transform.position, transform.localScale * secondaryAttackSize, 0, new Vector2(HandMovement.instance.dir.y, HandMovement.instance.dir.x * -1), 15, LayerMask.GetMask("Enemy"), 0);
            print(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                Destroy(a[i].collider.gameObject);
            }
            StartCoroutine(new UniversalTimer().Timer(0.2f, reset));
        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
