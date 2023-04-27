using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [HideInInspector] public float BaseSize = 1f;
    
    private Color _startColor;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _startColor = _renderer.sharedMaterial.color;
    }

    //Правильная смена цвета для GPU Instancing
    private void ChangeColor(Color newColor)
    {
        Debug.Log("Chenge color to " + newColor);
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor("_Color", newColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }

    //Создаёт кнопку в меню компонента в инспекторе
    [ContextMenu("ChangeColorOnRandom")]
    public void ChangeColorOnRandom()
    {
        ChangeColor(GetRandomColor());
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
        ChangeColor(_startColor);
    }

    private void ChangeSizeAnimation()
    {
        float animation = BaseSize + Mathf.Sin(Time.time * 8f) * BaseSize / 7f;
        transform.localScale = Vector3.one * animation;
    }
    
    public void ChangeSize()
    {
        transform.localScale = Vector3.one * BaseSize;
    }
}
