
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _isPauseMenuActive = false;
    private PlayerInputHandler _input;

    private void OnEnable()
    {
        if(PlayerInputHandler.Instance != null)
            PlayerInputHandler.Instance.PauseButtonPressed += TogglePauseMenu;
    }

    private void OnDisable()
    {
        if(PlayerInputHandler.Instance != null)
            PlayerInputHandler.Instance.PauseButtonPressed -= TogglePauseMenu;
    }

    private void TogglePauseMenu()
    {
        _isPauseMenuActive = !_isPauseMenuActive;
        if (_isPauseMenuActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerInputHandler.Instance.MovementInputActive = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerInputHandler.Instance.MovementInputActive = true;
        }
        _pauseMenu.gameObject.SetActive(_isPauseMenuActive);
    }
}
