using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastInteraction : MonoBehaviour
{
    [SerializeField] private float _rayLength = 10f;
    [SerializeField] private LayerMask _layerMaskInteractable;
    [SerializeField] private LayerMask _layerMaskKeyboard;
    private Camera _playerCamera;
    private GameObject _hitGameObject;
    public bool IsOnComputerCam = false;

    public delegate void FoundInteractableEventHandler(bool value);
    public event FoundInteractableEventHandler OnFoundInteractable;

    private void OnEnable()
    {
        if (PlayerInputHandler.Instance != null)
        {
            PlayerInputHandler.Instance.InteractButtonPressed += InteractWithObject;
            PlayerInputHandler.Instance.FireButtonPressed += FireSelectRay;
        }
    }

    private void OnDisable()
    {
        if (PlayerInputHandler.Instance != null)
        {
            PlayerInputHandler.Instance.InteractButtonPressed -= InteractWithObject;
            PlayerInputHandler.Instance.FireButtonPressed -= FireSelectRay;
        }
    }

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 localForward = _playerCamera.transform.TransformDirection(Vector3.forward);
        if (!IsOnComputerCam)
        {
            Debug.DrawRay(_playerCamera.transform.position, localForward * _rayLength, Color.magenta);
            if (Physics.Raycast(_playerCamera.transform.position, localForward, out hit, _rayLength, _layerMaskInteractable.value))
            {
                _hitGameObject = hit.collider.gameObject;
                OnFoundInteractable?.Invoke(true);
            }
            else
            {
                _hitGameObject = null;
                OnFoundInteractable?.Invoke(false);
            }
        }
    }

    public void EnterComputerInteraction()
    {
        OnFoundInteractable?.Invoke(false);
        IsOnComputerCam = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerInputHandler.Instance.MovementInputActive = false;
    }

    public void ExitComputerInteraction()
    {
        IsOnComputerCam = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerInputHandler.Instance.MovementInputActive = true;
    }

    void FireSelectRay()
    {
        if (!IsOnComputerCam) return;
        RaycastHit hit;
        Ray ray = _playerCamera.ScreenPointToRay(PlayerInputHandler.Instance.MousePosition);
        if (Physics.Raycast(ray, out hit, 10f, _layerMaskKeyboard.value))
        {
            Debug.Log("Found " + hit.collider.gameObject.name);
            var interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    void InteractWithObject()
    {
        if (_hitGameObject != null)
        {
            var interactable = _hitGameObject.GetComponent<IInteractable>();
            interactable.Interact();
        }
    }
}
