using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using MoreMountains.TopDownEngine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDash : NetworkBehaviour
{
    public enum DashTypes { Curve, Physics }

    public UnityEvent OnDashStart;
    public UnityEvent OnDashStop;
    
    [SerializeField] private float _dashForceForPhysicsType = 10f;
    [SerializeField] private bool _resetVelocityOnEndPhysicDash = true;
    [SerializeField] private DashTypes _dashType = DashTypes.Curve;
    [SerializeField] private Rigidbody _rigidbody;
    [Header("Dash")]
    [SerializeField] private Vector3 _dashDirection = Vector3.forward;
    [SerializeField] private float _dashDistance = 10f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private AnimationCurve _dashCurve = new AnimationCurve(new Keyframe(0f, 0f),
        new Keyframe(1f, 1f));

    protected bool _dashing;
    protected float _dashTimer;
    protected Vector3 _dashOrigin;
    protected Vector3 _dashDestination;
    protected Vector3 _newPosition;
    

    private void Awake()
    {
        Initialization();
    }

    private void Update()
    {
        HandleInput();
        
        ProcessAbility();
    }

    protected void Initialization()
    {
        
    }
    
    protected void HandleInput()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_dashing)
            {
                DashStart();
            }
        }
    }
    
    public virtual void DashStart()
    {
        Debug.Log("Dash start");
        OnDashStart.Invoke();
        float angle  = 0f;
        _dashing = true;
        _dashTimer = 0f;
        _dashOrigin = this.transform.position;

        switch (_dashType)
        {
            case DashTypes.Physics:
            {
                Vector3 dashDirection = transform.forward;
                _rigidbody.AddForce(_dashForceForPhysicsType * dashDirection, ForceMode.VelocityChange);
                break;
            }
            case DashTypes.Curve:
            {
                // angle = Vector3.SignedAngle(this.transform.forward, _controller.CurrentDirection.normalized, Vector3.up);
                //Добавить проверку попадания в стену
                _dashDestination = this.transform.position + _dashDirection.normalized * _dashDistance;
                // _dashDestination = MMMaths.RotatePointAroundPivot(_dashDestination, this.transform.position, _dashAngle);
                break;
            }
        }
    }

    public void ProcessAbility()
    {
        if (_dashing)
        {
            if (_dashTimer < _dashDuration)
            {
                if (_dashType == DashTypes.Curve)
                {
                    _newPosition = Vector3.Lerp(_dashOrigin, _dashDestination, _dashCurve.Evaluate(_dashTimer / _dashDuration));
                    transform.position = _newPosition; 
                }
                
                _dashTimer += Time.deltaTime;
            }
            else
            {
                DashStop();                   
            }
        }
    }
    
    protected virtual void DashStop()
    {
        Debug.Log("Dash stop");
        OnDashStop.Invoke();
        _dashing = false;
        if(_resetVelocityOnEndPhysicDash)
            _rigidbody.velocity = Vector3.zero;
    }
}
