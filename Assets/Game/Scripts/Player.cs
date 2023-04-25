using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 0.5f;
    
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movementVector3;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player " + gameObject.name + " made dash");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleMovement()
    {
        if (isLocalPlayer)
        {
            _moveHorizontal = Input.GetAxis("Horizontal");
            _moveVertical = Input.GetAxis("Vertical");
            _movementVector3 = new Vector3(_moveHorizontal, 0, _moveVertical);
        }
    }

    private void Move()
    {
        transform.position = transform.position + _movementVector3 * _moveSpeed;
    }
}
