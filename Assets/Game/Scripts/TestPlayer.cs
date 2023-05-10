using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private MovementTypes _movementType = MovementTypes.CharacterControllerMove;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _primaryMovementDirection;
    private bool _canMove = true;
    public enum MovementTypes {RigidbodyMovePosition, CharacterControllerMove}
    
    private void Update()
    {
        PrimaryMoveInput();
        Debug.Log(_primaryMovementDirection);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PrimaryMoveInput()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        _primaryMovementDirection = new Vector3(_moveHorizontal, 0, _moveVertical);
    }
    
    private void Move()
    {
        if(!_canMove) return;
        
        switch (_movementType)
        {
            case MovementTypes.RigidbodyMovePosition:
                _rigidbody.MovePosition(_rigidbody.position + _primaryMovementDirection * _moveSpeed);
                break;
            case MovementTypes.CharacterControllerMove:
                _characterController.Move(_primaryMovementDirection * _moveSpeed);
                break;
        }
    }
}
