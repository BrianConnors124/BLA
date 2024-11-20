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
    private float knockback;
    private float stun;
    

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
        reset += ResetSprite;
    }

    private void Presets()
    {
        name = obj.weaponName;
        primaryCD = obj.primaryCD;
        secondaryCD = obj.secondaryCD;
        damage = obj.baseDamage;
        attackSize = obj.baseReach / 10;
        description = obj.description;
        knockback = obj.knockback;
        stun = obj.stun;
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
        Vector2  a = new Vector2(HandScript.instance.dir.y, HandScript.instance.dir.x * -1);
        BoxCastDrawer.BoxCastAndDraw(transform.position, new Vector2(2,2) * attackSize, 0, a, attackSize * 5 );  
        BoxCastDrawer.BoxCastAndDraw(transform.position,new Vector2(2,2) * attackSize, 0, a,0);  
        
    }

    
    
    
    private void Primary()
    {
        if (primaryCDT.TimerDone)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCooldowns();
            Vector2 b = new Vector2(HandScript.instance.dir.y, HandScript.instance.dir.x * -1);
            RaycastHit2D a = Physics2D.BoxCast(transform.position,new Vector2(2,2) * attackSize, 0, b,0, LayerMask.GetMask("Enemy"));
            a.collider.GetComponent<EnemyController>().DamageDelt(damage, knockback, stun);
            StartCoroutine(new UniversalTimer().Timer((primaryCD / 2), reset));  
        }
    }
    
    
    private void Secondary()
    {
        if (secondaryCDT.TimerDone && primaryCDT.TimerDone)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCooldowns();
            Vector2 a = new Vector2(HandScript.instance.dir.y, HandScript.instance.dir.x * -1);
            hit = Physics2D.BoxCastAll(transform.position, new Vector2(1,1) * attackSize * 2, 0, a, attackSize * 5 , LayerMask.GetMask("Enemy" ));
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].collider.GetComponent<EnemyController>().DamageDelt(damage * 1.2f, knockback, stun);
            }
            StartCoroutine(new UniversalTimer().Timer((primaryCD / 2), reset));
        }
    }

    private void ResetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private void StartCooldowns()
    {
        StartCoroutine(primaryCDT.Timer(primaryCD));
        StartCoroutine(secondaryCDT.Timer(secondaryCD));
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
