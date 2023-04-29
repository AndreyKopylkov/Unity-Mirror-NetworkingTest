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
    
    [SyncVar]
    private List<PlayerScoreData> _playersScoreDataList = new List<PlayerScoreData>();

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

    private void AddPlayer(GameObject player)
    {
        PlayerScoreData playerScoreData = new PlayerScoreData();
        playerScoreData.PlayerGO = player;
        playerScoreData.PlayerID = player.name;
        playerScoreData.Score = 0;
        
        _playersScoreDataList.Add(playerScoreData);
        SetText();
    }

    private void AddScore(GameObject player)
    {
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

    private void SetText()
    {
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
    private GameObject _playerGO;
    private string _playerID;
    private int _score;

    public GameObject PlayerGO { get => _playerGO; set => _playerGO = value; }
    public string PlayerID { get => _playerID; set => _playerID = value; }
    public int Score { get => _score; set => _score = value; }
}
