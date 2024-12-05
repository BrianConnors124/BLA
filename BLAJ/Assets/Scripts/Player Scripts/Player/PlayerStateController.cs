using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    enum PlayerStates{Idle,Running,Jump,Attack,Airborne}

    private PlayerStates state;
    private bool stateComplete;
    private float movement;

    private bool TouchingGround => GameObject.Find("PlayerGroundCheck").GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("WorldObj"));

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (state)
        {
            case PlayerStates.Idle:

                break;
            case PlayerStates.Airborne:

                break;
            case PlayerStates.Attack:

                break;
            case PlayerStates.Running:

                break;
            case PlayerStates.Jump:

                break;
        }

        movement = InputSystemController.MovementInput().magnitude;
    }
    
    private void UpdateIdle()
    {
        if (movement > 0)
        {
            stateComplete = true;
        }
    }
    
    private void UpdateAirborne()
    {
        if (TouchingGround)
        {
            stateComplete = true;
        }
    }
    private void UpdateRunning()
    {
        if (!TouchingGround || movement > 0)
        {
            stateComplete = true;
        }
    }

    private void UpdateAttack()
    {
        
    }
    
    
    
}

