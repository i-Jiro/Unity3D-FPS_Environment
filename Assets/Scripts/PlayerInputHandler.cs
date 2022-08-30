using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;
    public Vector2 LookInput { get; private set; } = Vector2.zero;
    public static PlayerInputHandler Instance;
    public FirstPersonInput Inputs { get; private set; }

    public delegate void InteractButtonEventHandler();
    public event InteractButtonEventHandler InteractButtonPressed;
    

    //Register to C# events.
    private void OnEnable()
    {
        Inputs = new FirstPersonInput();
        Inputs.FirstPerson.Move.performed += SetMove;
        Inputs.FirstPerson.Move.canceled += SetMove;
        Inputs.FirstPerson.Look.performed += SetLook;
        Inputs.FirstPerson.Look.canceled += SetLook;
        Inputs.FirstPerson.Interact.performed += OnInteractPress;
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
    
    private void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void OnInteractPress(InputAction.CallbackContext ctx)
    {
        InteractButtonPressed.Invoke();
    }
}
