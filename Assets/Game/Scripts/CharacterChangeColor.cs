using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterChangeColor : MonoBehaviour
{
    [SerializeField] private string _colorPropertiesName = "_Color";
    [SerializeField] private Renderer _renderer;

    private Color _startColor;
    private MaterialPropertyBlock _materialPropertyBlock;
    private bool _colorChanged = false;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Reset();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Initialize()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _startColor = _renderer.sharedMaterial.color;
    }

    private bool ChangeColorOnNew(Color newColor)
    {
        if (_colorChanged)
        {
            Debug.Log("The color has already been changed");
            return false;
        }
        
        ChangeColor(newColor);
        Debug.Log("Change color to " + newColor);
        _colorChanged = true;
        return true;
    }

    private void ChangeColor(Color newColor)
    {
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor(_colorPropertiesName, newColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    public void ChangeColorOnTime(Color targetColor, float duration)
    {
        if(ChangeColorOnNew(targetColor)) StartCoroutine(ResetColorAfterDelayCoroutine(duration));
    }

    private IEnumerator ResetColorAfterDelayCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        Reset();
    }
    
    [ContextMenu("ChangeColorOnRandom")]
    public void ChangeColorOnRandom()
    {
        ChangeColorOnNew(GetRandomColor());
    }

    private Color GetRandomColor()
    {
        Color randomColor = new Color(Random.Range(0f, 1f),
            Random.Range(0f, 1f), Random.Range(0f, 1f));
        return randomColor;
    }
    
    [ContextMenu("Reset")]
    public void Reset()
    {
        Debug.Log("Reset on " + this.gameObject);
        _colorChanged = false;
        ChangeColor(_startColor);
    }
}
