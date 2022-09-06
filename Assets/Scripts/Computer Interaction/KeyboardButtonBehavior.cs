using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButtonBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private string _key;
    public delegate void KeyboardButtonPressEventHandler(string key);
    public event KeyboardButtonPressEventHandler ButtonPressed;

    public void Interact()
    {
        OnButtonPressed();
    }

    private void OnButtonPressed()
    {
        Debug.Log(_key);
        ButtonPressed?.Invoke(_key);
    }
}
