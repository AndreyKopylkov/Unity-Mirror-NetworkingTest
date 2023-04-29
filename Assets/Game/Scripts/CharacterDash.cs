using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDash : NetworkBehaviour
{
    public event Action OnDashStart;
    public event Action OnDashStop;
    
    [SerializeField] private float _dashForceForPhysicsType = 10f;
    [SerializeField] private bool _resetVelocityOnEndPhysicDash = true;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _cooldownTime = 1f;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _additionalYAngel = 0;

    private Coroutine _dashingCoroutine;

    private void Awake()
    {
        Initialization();
    }

    private void OnEnable()
    {
        _playerInput.OnDashPush += TryDash;
    }

    private void OnDisable()
    {
        _playerInput.OnDashPush -= TryDash;
    }
    
    private void Initialization()
    {
        
    }
    
    private void TryDash()
    {
        if (isLocalPlayer)
        {
            if (_playerInput && _dashingCoroutine == null)
            {
                DashStart();
            }
        }
    }
    
    private void DashStart()
    {
        Debug.Log("Dash start");
        
        OnDashStart?.Invoke();
        _playerMovement.CanMove = false;

        Vector3 cameraForward = _camera.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        Quaternion rotation = Quaternion.LookRotation(cameraForward);

        Vector3 dashDirection;
        if (_playerInput.PrimaryMovementDirection == Vector3.zero)
            dashDirection = cameraForward;
        else
            dashDirection = (rotation * _playerInput.PrimaryMovementDirection).normalized;
        dashDirection.y += _additionalYAngel;
        _rigidbody.AddForce(_dashForceForPhysicsType * dashDirection, ForceMode.VelocityChange);
        // _characterController.Move(_dashForceForPhysicsType * dashDirection);
        _dashingCoroutine = StartCoroutine(DashingCoroutine());
    }

    private IEnumerator DashingCoroutine()
    { 
        float dashTimer = 0;

        while (dashTimer < _dashDuration)
        {
            dashTimer += Time.deltaTime;
            yield return null;
        }
        
        DashStop();

        yield return new WaitForSeconds(_cooldownTime);
        _dashingCoroutine = null;
    }
    
    private void DashStop()
    {
        Debug.Log("Dash stop");
        OnDashStop?.Invoke();
        _characterController.transform.position = transform.position;
        _playerMovement.CanMove = true;
        if(_resetVelocityOnEndPhysicDash)
            _rigidbody.velocity = Vector3.zero;
    }
}
