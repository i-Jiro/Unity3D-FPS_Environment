using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComputerBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private string _displayText;
    [SerializeField] private GameObject _vCamComputer;
    [SerializeField] private TextMeshProUGUI _screenTextField;
    private bool _isInteracting;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _keyboardButtonSound;
    private void OnEnable()
    {
        var buttons = GetComponentsInChildren<KeyboardButtonBehavior>();
        foreach (var button in buttons)
        {
            button.ButtonPressed += AddText;
        }

        PlayerInputHandler.Instance.AltFireButtonPressed += EndInteraction;
    }

    private void OnDisable()
    {
        var buttons = GetComponentsInChildren<KeyboardButtonBehavior>();
        foreach (var button in buttons)
        {
            button.ButtonPressed -= AddText;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _vCamComputer.SetActive(false);
    }

    public void Interact()
    {
        _vCamComputer.SetActive(true);
        GameObject.FindObjectOfType<PlayerRaycastInteraction>()?.EnterComputerInteraction();
        _isInteracting = true;
    }

    private void EndInteraction()
    {
        if(_isInteracting)
        {
            _vCamComputer.SetActive(false);
            GameObject.FindObjectOfType<PlayerRaycastInteraction>()?.ExitComputerInteraction();
            _isInteracting = false;
        }
    }

    private void AddText(string text)
    {
        _audioSource.PlayOneShot(_keyboardButtonSound);
        _displayText += text;
        _screenTextField.text = _displayText;
    }
}
