using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SearchService;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Timer = Unity.VisualScripting.Timer;

public class WeaponScript : MonoBehaviour
{
    [Header("WeaponType")] 
    public WeaponInfo obj;
    private float attackSize;
    private float primaryCD;
    private float secondaryCD;
    private float damage;
    private string name;
    private string description;
    

    [Header("Timer")]
    private UniversalTimer primaryCDT;
    private UniversalTimer secondaryCDT;
    
    [Header("Misc")]
    private Rigidbody2D rb;
    private Action reset;
    private RaycastHit2D[] hit;
    private Action primaryAttack;
    private Action secondaryAttack;
    
    
    
    
    
    void Start()
    {
        Actions();
        Presets();
        StartCoroutine(new UniversalTimer().Timer(.5f, Print));
        
    }

    private void Presets()
    {
        name = obj.weaponName;
        primaryCD = obj.primaryCD;
        secondaryCD = obj.secondaryCD;
        damage = obj.baseDamage;
        attackSize = obj.baseReach;
        description = obj.description;
    }
    
    private void Actions()
    {
        InputSystemController.instance.primaryAction += Primary;
        InputSystemController.instance.secondaryAction += Secondary;
        primaryCDT = new UniversalTimer();
        primaryCDT.Reset();
        secondaryCDT = new UniversalTimer();
        secondaryCDT.Reset();
    }
    private void Update()
    {
        Vector2 a = new Vector2(HandScript.instance.dir2.y, HandScript.instance.dir2.x * -1);
        if (PlayerController.instance.armMovesWithMovement)
        {
            a = new Vector2(HandScript.instance.dir.y, HandScript.instance.dir.x * -1);
        }
        BoxCastDrawer.BoxCastAndDraw(transform.position, new Vector2(2,2) * attackSize, 0, a, attackSize * 3);  
        
    }

    
    
    
    private void Primary()
    {
        if (primaryCDT.TimerDone)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(primaryCDT.Timer(primaryCD));
            RaycastHit2D[] a = Physics2D.CircleCastAll(transform.position, attackSize,transform.position * PlayerController.instance.direction, attackSize, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < a.Length; i++)
            {
             print(a[i].collider);
             Destroy(a[i].collider.gameObject);
            }
            // StartCoroutine(new UniversalTimer().Timer((primaryCD / 2), reset));   
        }
    }
    
    
    private void Secondary()
    {
        if (secondaryCDT.TimerDone && primaryCDT.TimerDone)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(secondaryCDT.Timer(secondaryCD));
            StartCoroutine(primaryCDT.Timer(primaryCD));
            Vector2 a = new Vector2(HandScript.instance.dir2.y, HandScript.instance.dir2.x * -1);
            if (PlayerController.instance.armMovesWithMovement)
            {
                a = new Vector2(HandScript.instance.dir.y, HandScript.instance.dir.x * -1);
            }
            hit = Physics2D.BoxCastAll(transform.position, new Vector2(1,1) * attackSize * 2, 0, a, attackSize * 5 , LayerMask.GetMask("Enemy"), 0);
            print(hit.Length);
            for (int i = 0; i < hit.Length; i++)
            {
                Destroy(hit[i].collider.gameObject);
            }
            // StartCoroutine(new UniversalTimer().Timer((primaryCD / 2), reset));
        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackSize);
    }

    private void Print()
    {
        print("Name: " + name + ", " + description + "\nDamage: " + damage + " Reach: " + attackSize);
    }
}
