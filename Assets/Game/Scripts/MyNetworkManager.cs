using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server Started!");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Debug.Log("Server Stopped!");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Connected to Server!");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
        
        Debug.Log("Send event - SendPlayerConnect");
        GlobalEventManager.SendPlayerConnect(player);
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("Disconnected from Server!");
    }
}
