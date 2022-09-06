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
    private void OnEnable()
    {
        var buttons = GetComponentsInChildren<KeyboardButtonBehavior>();
        foreach (var button in buttons)
        {
            button.ButtonPressed += AddText;
        }
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
        _vCamComputer.SetActive(false);
    }

    public void Interact()
    {
        _vCamComputer.SetActive(true);
        GameObject.FindObjectOfType<PlayerRaycastInteraction>().EnterComputerInteraction();
    }

    private void AddText(string text)
    {
        _displayText += text;
        _screenTextField.text = _displayText;
    }
}
