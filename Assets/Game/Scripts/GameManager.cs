using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 30;

    private void Awake()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
