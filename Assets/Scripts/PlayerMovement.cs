using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _input;
    [SerializeField] private float _movementSpeed = 5f;
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        Vector3 localDirection = transform.TransformDirection(_direction);
        //_rigidbody.AddForce(localDirection.normalized * _movementSpeed, ForceMode.Force);
        _rigidbody.MovePosition(_rigidbody.position + (localDirection * _movementSpeed * Time.deltaTime));
    }

    void GetInput()
    {
        _direction.x = _input.MoveInput.x;
        _direction.z = _input.MoveInput.y;
    }
}
