using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private Rigidbody _rigidbody;

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movementVector3;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        DashInput();
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
        // transform.position = transform.position + _movementVector3 * _moveSpeed;
        _rigidbody.MovePosition(transform.position + _movementVector3 * _moveSpeed);
    }

    private void DashInput()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }
    }
    
    //Вызывается на клиенте - работает на сервере
    [Command]
    private void Dash()
    {
        Debug.Log("Sending this command to Server");
        Debug.Log("Player " + gameObject.name + " made dash");
    }

    //Вызывается на сервере - работает на всех клиентах
    [ClientRpc]
    private void WorkInClients()
    {
        
    }
    
    //Вызывается на сервере - работает на клиенте
    [TargetRpc]
    private void WorkInTargetClient()
    {
        
    }
}
