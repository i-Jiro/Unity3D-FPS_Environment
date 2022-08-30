using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehavior : MonoBehaviour,IInteractable
{
    private Light _light;
    private bool _isOn = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponentInChildren<Light>();
    }
    
    public void Interact()
    {
        if (_isOn)
            _light.enabled = false;
        else
        {
            _light.enabled = true;
        }
        _isOn = !_isOn;
    }
}
