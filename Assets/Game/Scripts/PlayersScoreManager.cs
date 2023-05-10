using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayersScoreManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTMP;
    
    private SyncList<PlayerScoreData> _playersScoreDataList = new SyncList<PlayerScoreData>();

    // public delegate void PlayersScoreDataListChanged(List<PlayerScoreData> playersScoreDataList);

    private void Awake()
    {
        GlobalEventManager.OnPlayerChangeColor += AddScore;
        GlobalEventManager.OnPlayerConnect += AddPlayer;
        Initialize();
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnPlayerChangeColor -= AddScore;
        GlobalEventManager.OnPlayerConnect -= AddPlayer;
    }

    private void Initialize()
    {
        SetText();
    }

    [Server]
    private void AddPlayer(GameObject player)
    {
        if (!isOwned) return;
        
        Debug.Log("AddPlayer");
        PlayerScoreData playerScoreData = new PlayerScoreData();
        playerScoreData.PlayerGO = player;
        playerScoreData.PlayerID = player.name;
        playerScoreData.Score = 0;
        
        _playersScoreDataList.Add(playerScoreData);
        SetText();
    }

    [Server]
    private void AddScore(GameObject player)
    {
        Debug.Log("AddScore");
        foreach (var playerScoreData in _playersScoreDataList)
        {
            if (playerScoreData.PlayerGO == player)
            {
                playerScoreData.Score++;
                SetText();
                return;
            }
        }
    }
    
    [ClientRpc]
    [ContextMenu("SetText")]
    private void SetText()
    {
        if(!isClient)
            return;
        
        Debug.Log("Set Text!!!");
        string newText = "Score: ";
        foreach (var playerScoreData in _playersScoreDataList)
        {
            newText += ("\n" + playerScoreData.PlayerID + " - " + playerScoreData.Score);
        }
        _scoreTMP.SetText(newText);
    }
}

public class PlayerScoreData
{
    [SyncVar] private GameObject _playerGO;
    [SyncVar] private string _playerID;
    [SyncVar] private int _score;

    public GameObject PlayerGO { get => _playerGO; set => _playerGO = value; }
    public string PlayerID { get => _playerID; set => _playerID = value; }
    public int Score { get => _score; set => _score = value; }
}
