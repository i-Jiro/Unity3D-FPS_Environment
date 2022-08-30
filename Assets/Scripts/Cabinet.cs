using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour, IInteractable
{
    private Animator _animator;
    private bool _isOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        if (!_isOpen)
        {
            Debug.Log("Opened.");
            _isOpen = true;
        }
        else
        {
            Debug.Log("Closed.");
            _isOpen = false;
        }
    }
}
