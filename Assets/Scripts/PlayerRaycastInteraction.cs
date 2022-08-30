using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastInteraction : MonoBehaviour
{
    [SerializeField] private float _rayLength = 10f;
    [SerializeField] private LayerMask _layerMaskInteractable;
    private Camera _playerCamera;
    private GameObject hitGameObject;

    public delegate void FoundInteractableEventHandler(bool value);
    public event FoundInteractableEventHandler OnFoundInteractable;

    private void OnEnable()
    {
        if(PlayerInputHandler.Instance != null)
            PlayerInputHandler.Instance.InteractButtonPressed += InteractWithObject;
    }

    private void OnDisable()
    {
        if(PlayerInputHandler.Instance != null)
            PlayerInputHandler.Instance.InteractButtonPressed += InteractWithObject;
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
        Debug.DrawRay(_playerCamera.transform.position, localForward * _rayLength, Color.magenta);
        if (Physics.Raycast(_playerCamera.transform.position, localForward, out hit, _rayLength, _layerMaskInteractable.value))
        {
            hitGameObject = hit.collider.gameObject;
            OnFoundInteractable?.Invoke(true);
        }
        else
        {
            hitGameObject = null;
            OnFoundInteractable?.Invoke(false);
        }
    }

    void InteractWithObject()
    {
        if (hitGameObject != null)
        {
            var interactable = hitGameObject.GetComponent<IInteractable>();
            interactable.Interact();
        }
    }
}
