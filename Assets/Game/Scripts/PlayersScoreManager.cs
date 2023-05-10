using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class PlayersScoreManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTMP;

    private SyncDictionary<GameObject, int> _playerScoreDictionary = new SyncDictionary<GameObject, int>();

    // private readonly SyncList<PlayerScoreData> _playersScoreDataList = new SyncList<PlayerScoreData>();
    
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
        // SetText();
    }

    [Server]
    private void AddPlayer(GameObject player)
    {
        Debug.Log("-----------AddPlayer");

        // if (!isOwned) return;
        
        Debug.Log("AddPlayer");
        // PlayerScoreData playerScoreData = new PlayerScoreData();
        // playerScoreData.PlayerGO = player;
        // playerScoreData.PlayerID = player.name;
        // playerScoreData.Score = 0;
        // _playersScoreDataList.Add(playerScoreData);
        // PlayerScoreDataReaderWriter.WritePlayerScoreData(, _playersScoreDataList);

        _playerScoreDictionary.Add(player, 0);
        
        SetText();
    }

    [Server]
    private void AddScore(GameObject player)
    {
        // if (!isOwned) return;

        Debug.Log("AddScore");
        // foreach (var playerScoreData in _playersScoreDataList)
        // {
        //     if (playerScoreData.PlayerGO == player)
        //     {
        //         playerScoreData.Score++;
        //         SetText();
        //         return;
        //     }
        // }

        int newScore = _playerScoreDictionary[player]++;
        _playerScoreDictionary[player] = newScore;
        Debug.Log("New Score: " + _playerScoreDictionary[player]);
    }
    
    [ContextMenu("SetText")]
    [ClientRpc]
    private void SetText()
    {
        // if(!isClient)
        //     return;
        
        Debug.Log("Set Text!!!");
        string newText = "Score: ";
        // foreach (var playerScoreData in _playersScoreDataList)
        // {
        //     newText += ("\n" + playerScoreData.PlayerID + " - " + playerScoreData.Score);
        // }
        foreach (var playerScore in _playerScoreDictionary)
        {
            newText += ("\n" + playerScore.Key.name + " - " + playerScore.Value);
        }
        _scoreTMP.SetText(newText);
    }
}

// public class PlayerScoreData
// {
//     [SyncVar] private GameObject _playerGO;
//     [SyncVar] private string _playerID;
//     [SyncVar] private int _score;
//
//     public GameObject PlayerGO { get => _playerGO; set => _playerGO = value; }
//     public string PlayerID { get => _playerID; set => _playerID = value; }
//     public int Score { get => _score; set => _score = value; }
// }
//
// public static class PlayerScoreDataReaderWriter
// {
//     public static void WritePlayerScoreData(this NetworkWriter writer, PlayerScoreData playerScoreData)
//     {
//         writer.WriteGameObject(playerScoreData.PlayerGO);
//         writer.WriteString(playerScoreData.PlayerID);
//         writer.WriteInt(playerScoreData.Score);
//     }
//
//     public static PlayerScoreData ReadPlayerScoreData(this NetworkReader reader)
//     {
//         return new PlayerScoreData
//         {
//             PlayerGO = reader.ReadGameObject(),
//             PlayerID = reader.ReadString(),
//             Score = reader.ReadInt()
//         };
//     }
// }


