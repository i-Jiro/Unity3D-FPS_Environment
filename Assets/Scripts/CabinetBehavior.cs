using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetBehavior : MonoBehaviour, IInteractable
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
            _animator.SetBool("IsOpen", true);
            _isOpen = true;
        }
        else
        {
            _animator.SetBool("IsOpen", false);
            _isOpen = false;
        }
    }
}
