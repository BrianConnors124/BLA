using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputSystemController : MonoBehaviour
{

    public static InputSystemController instance;
    public PlayerInput playerInput;
    public string[] actionMaps;
    
    private bool queueActive;
    private bool buttonPressed;
    private UniversalTimer _queueTimer;

    [Header("ActionKeys")] 
    private string jumpKey = "Jump";
    private string attackKey = "Attack";
    private string superAttackKey = "SuperAttack";
    private string dashKey = "Dash";

    [SerializeField] private InputActionReference walk;
    
    public Action openInventory;
    public Action nextSequence;
    public Action pauseMenu;
    public Action useItem;
    public Action selectItem;
    public Action unselectItem;
    public Action sortItem;


    private void Awake()
    {
        instance = this;
        _queueTimer = GetComponent<UniversalTimer>();
        

    }

    public static Vector2 MovementInput() => instance.walk.action.ReadValue<Vector2>();

public void HandleJump(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(jumpKey, 0.1f);
        
    }  
    public bool TryingJump() => _queueTimer.TimerActive(jumpKey);
    
    
    public void HandleDash(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(dashKey, 0.1f);
        
    } 
    public bool TryingDash() => _queueTimer.TimerActive(dashKey);
    
    public void HandleAttack(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(attackKey, 0.1f);
        
    } 
    
    public bool TryingAttack() => _queueTimer.TimerActive(attackKey);
    
    public void HandleSuperAttack(InputAction.CallbackContext context)
    {
        
        if(context.performed)_queueTimer.SetTimer(superAttackKey, 0.1f);
        
    } 
    
    public bool TryingSuperAttack() => _queueTimer.TimerActive(superAttackKey);
    
    
    //~~~~~~~~~~~~~~~Inventory~~~~~~~~~~~~~~~~\\
    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed) openInventory.Invoke();
    }
    
    public void NextTutorialSequence(InputAction.CallbackContext context)
    {
        if (context.performed) nextSequence.Invoke();
    }
    
    public void TogglePauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed) pauseMenu.Invoke();
    }

    public void HandleItemUsage(InputAction.CallbackContext context)
    {
        if(context.performed) print("Use Item");
    }
    public void HandleItemSort(InputAction.CallbackContext context)
    {
        if(context.performed) print("Sort Item");
    }

    public void HandleItemSelect(InputAction.CallbackContext context)
    {
        if(context.performed) selectItem.Invoke();
        if(context.canceled) unselectItem.Invoke();
    }
    
}
