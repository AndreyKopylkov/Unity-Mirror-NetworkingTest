using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager
{
    public static event Action<GameObject> OnPlayerChangeColor;
    public static event Action<GameObject> OnPlayerConnect;

    public static void SendPlayerChangeColor(GameObject player)
    { 
        OnPlayerChangeColor?.Invoke(player);
    }
    
    public static void SendPlayerConnect(GameObject player)
    { 
        OnPlayerConnect?.Invoke(player);
    }
}
