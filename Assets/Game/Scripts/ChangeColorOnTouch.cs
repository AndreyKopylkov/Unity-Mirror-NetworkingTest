using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTouch : MonoBehaviour
{
    [SerializeField] private float _colorChangeDuration = 3f;
    [SerializeField] private Color _targetColor = Color.blue;
    [SerializeField] private CharacterChangeColor _characterOwner;
    [SerializeField] private CharacterDash _characterDash;
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        DisableCollider();
    }

    private void OnEnable()
    {
        _characterDash.OnDashStart += EnableCollider;
        _characterDash.OnDashStop += DisableCollider;
    }

    private void OnDisable()
    {
        _characterDash.OnDashStart -= EnableCollider;
        _characterDash.OnDashStop -= DisableCollider;
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
    }

    private void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        CharacterChangeColor characterChangeColor = other.GetComponent<CharacterChangeColor>();
        if(characterChangeColor) ChangeColor(characterChangeColor);
    }

    private void ChangeColor(CharacterChangeColor characterChangeColor)
    {
        if(_characterOwner == characterChangeColor) return;
        
        characterChangeColor.ChangeColorOnTime(_targetColor, _colorChangeDuration, _characterOwner.gameObject);
    }
}
