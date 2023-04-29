using System;
using Mirror;
using UnityEngine;

public class ThirdPersonCameraController : NetworkBehaviour
{
    public enum RotationTypes {Instant, Smooth};
    
    [SerializeField] private Transform _player;
    [SerializeField] private float _distance = 5.0f;
    [SerializeField] private float _height = 2.0f;
    [SerializeField] private float _sensitivity = 3.0f;
    [SerializeField] private float _limitUp = 80.0f;
    [SerializeField] private float _limitDown = -20.0f;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private RotationTypes _rotationType = RotationTypes.Smooth;
    [SerializeField] private float _playerRotateSpeed = 10;
    
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        MoveCamera();
        _camera.transform.SetParent(transform);
    }

    void Update()
    {
        if(!isLocalPlayer) return;
        
        RotateCamera();
    }
    
    private void LateUpdate()
    {
        if(!isLocalPlayer) return;

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        switch (_rotationType)
        {
            case RotationTypes.Instant:
                _player.transform.rotation = Quaternion.Euler(_player.transform.rotation.x, _rotationX,
                    _player.transform.rotation.z);
                break;
            case RotationTypes.Smooth:
                Vector3 cameraForward = _camera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, targetRotation,
                    Time.deltaTime * _playerRotateSpeed);
                break;
        }
    }

    private void MoveCamera()
    {
        _camera.transform.position = _player.position - transform.rotation * Vector3.forward * _distance + Vector3.up * _height;
    }

    private void RotateCamera()
    {
        _rotationX += _playerInput.SecondaryMovementDirection.x * _sensitivity;
        _rotationY -= _playerInput.SecondaryMovementDirection.y * _sensitivity;
        _rotationY = Mathf.Clamp(_rotationY, _limitUp, _limitDown);
    
        _camera.transform.rotation = Quaternion.Euler(_rotationY, _rotationX, 0);
    }
}
