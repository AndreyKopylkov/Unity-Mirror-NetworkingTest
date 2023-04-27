using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTouch : MonoBehaviour
{
    [SerializeField] private float _colorChangeDuration = 3f;
    [SerializeField] private Color _targetColor = Color.blue;
    [SerializeField] private CharacterChangeColor _characterOwner;

    private void OnTriggerStay(Collider other)
    {
        CharacterChangeColor characterChangeColor = other.GetComponent<CharacterChangeColor>();
        if(characterChangeColor) ChangeColor(characterChangeColor);
    }

    private void ChangeColor(CharacterChangeColor characterChangeColor)
    {
        if(_characterOwner) return;
        
        characterChangeColor.ChangeColorOnTime(_targetColor, _colorChangeDuration);
    }
}
