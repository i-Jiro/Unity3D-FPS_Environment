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
    private FirstPersonInput _inputs;

    //Register to C# events.
    private void OnEnable()
    {
        _inputs = new FirstPersonInput();
        _inputs.FirstPerson.Move.performed += SetMove;
        _inputs.FirstPerson.Move.canceled += SetMove;
        _inputs.FirstPerson.Look.performed += SetLook;
        _inputs.FirstPerson.Look.canceled += SetLook;
        _inputs.Enable();

    }

    //Unsubscribe to events.
    private void OnDisable()
    {
        _inputs.FirstPerson.Move.performed -= SetMove;
        _inputs.FirstPerson.Move.canceled -= SetMove;
        _inputs.FirstPerson.Look.performed -= SetLook;
        _inputs.FirstPerson.Look.canceled -= SetLook;
        _inputs.Disable();
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

}
