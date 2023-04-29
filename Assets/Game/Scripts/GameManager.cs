using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _targetFPS = 30;

    private void Awake()
    {
        Application.targetFrameRate = _targetFPS;
    }

    public void StopGame()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
            MyNetworkManager.singleton.StopHost();
        else if(NetworkClient.isConnected)
            MyNetworkManager.singleton.StopClient();
        else if(NetworkServer.active)
            NetworkManager.singleton.StopServer();
    }
}
