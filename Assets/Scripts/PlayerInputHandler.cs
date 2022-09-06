using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public Vector2 MousePosition { get; private set; } = Vector2.zero;
    public static PlayerInputHandler Instance;
    public FirstPersonInput Inputs { get; private set; }

    public bool MovementInputActive = true;
    public delegate void InteractButtonEventHandler();
    public event InteractButtonEventHandler InteractButtonPressed;
    public delegate void PauseButtonEventHandler();
    public event PauseButtonEventHandler PauseButtonPressed;
    public delegate void FireButtonEventHandler();
    public event FireButtonEventHandler FireButtonPressed;
    public delegate void AltFireButtonEventHandler();
    public event AltFireButtonEventHandler AltFireButtonPressed;
    

    //Register to C# events.
    private void OnEnable()
    {
        Inputs = new FirstPersonInput();
        Inputs.FirstPerson.Move.performed += SetMove;
        Inputs.FirstPerson.Move.canceled += SetMove;
        Inputs.FirstPerson.Look.performed += SetLook;
        Inputs.FirstPerson.Look.canceled += SetLook;
        Inputs.FirstPerson.Interact.performed += OnInteractPress;
        Inputs.FirstPerson.Pause.performed += OnPausePress;
        Inputs.FirstPerson.Select.performed += SetSelect;
        Inputs.FirstPerson.Fire.performed += OnPrimaryFire;
        Inputs.FirstPerson.AltFire.performed += OnAltFire;
        Inputs.Enable();
    }

    //Unsubscribe to events.
    private void OnDisable()
    {
        Inputs.FirstPerson.Move.performed -= SetMove;
        Inputs.FirstPerson.Move.canceled -= SetMove;
        Inputs.FirstPerson.Look.performed -= SetLook;
        Inputs.FirstPerson.Look.canceled -= SetLook;
        Inputs.FirstPerson.Interact.performed -= OnInteractPress;
        Inputs.FirstPerson.Select.performed -= SetSelect;
        Inputs.FirstPerson.Fire.performed -= OnPrimaryFire;
        Inputs.Disable();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SetSelect(InputAction.CallbackContext ctx)
    {
        MousePosition = ctx.ReadValue<Vector2>();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        if(MovementInputActive)
            MoveInput = ctx.ReadValue<Vector2>();
        else
        {
            MoveInput = Vector2.zero;
        }
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        if (MovementInputActive)
        {
            LookInput = ctx.ReadValue<Vector2>();
        }
        else
        {
            LookInput = Vector2.zero;
        }
    }

    private void OnInteractPress(InputAction.CallbackContext ctx)
    {
        InteractButtonPressed?.Invoke();
    }

    private void OnPausePress(InputAction.CallbackContext ctx)
    {
        PauseButtonPressed?.Invoke();
    }

    private void OnPrimaryFire(InputAction.CallbackContext ctx)
    {
        FireButtonPressed?.Invoke();
    }
    
    private void OnAltFire(InputAction.CallbackContext ctx)
    {
        AltFireButtonPressed?.Invoke();
    }
    
}
