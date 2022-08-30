using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerRaycastInteraction _playerRaycastInteraction;
    [SerializeField] private GameObject _interactText;

    private void OnEnable()
    {
        _playerRaycastInteraction.OnFoundInteractable += ToggleInteractText;
    }

    private void OnDisable()
    {
        _playerRaycastInteraction.OnFoundInteractable -= ToggleInteractText;
    }

    private void ToggleInteractText(bool value)
    {
        _interactText.SetActive(value);   
    }
}
