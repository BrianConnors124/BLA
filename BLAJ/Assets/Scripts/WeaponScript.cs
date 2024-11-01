using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
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
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        InputSystemController.instance.primaryAction += Primary;
        InputSystemController.instance.primaryAction += Secondary;
    }
    
    
    // Primary/Secondary Attack ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public static RaycastHit2D CircleCast()
    //RaycastHit2D CircleCastAll for AOE attacks
    //RaycastHit2D CircleCast for single attacks
    private void Primary()
    {
        //primary will likely be a single attack for most classes
        //RaycastHit2D CircleCast(new Vector2())
        Physics2D.CircleCastAll(transform.position, attackRadius,
            new Vector2(NegOrPos(transform.position.x), transform.position.y));
    }

    private void Secondary()
    {
        //secondary will likely be an AOE attack for some classes
    }

    private float NegOrPos(float a)
    {
        return a / math.abs(a);
    }
}
