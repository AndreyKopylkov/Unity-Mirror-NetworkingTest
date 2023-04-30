using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GlobalEventManager
{
    public static event Action<GameObject> OnPlayerChangeColor;
    public static event Action<GameObject> OnPlayerConnect;

    public static void SendPlayerChangeColor(GameObject player)
    { 
        Debug.Log("Event SendPlayerChangeColor");
        OnPlayerChangeColor?.Invoke(player);
    }
    
    public static void SendPlayerConnect(GameObject player)
    { 
        Debug.Log("Event SendPlayerConnect");
        OnPlayerConnect?.Invoke(player);
    }
}
