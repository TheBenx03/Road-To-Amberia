using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerList : NetworkBehaviour
{
    public static PlayerList instance;

    // Store user IDs and names
    private readonly Dictionary<int, string> playerDic = new();

    // Instance class
    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogWarning("Another instance of PlayerList already exists!");
    }

    // Add user to the list
    public void Add(int connectionId, string username) =>playerDic.Add(connectionId, username);

    // Remove user from the list
    public void Remove(int connectionId) => playerDic.Remove(connectionId);

    // Get list of all users
    public Dictionary<int, string> Get() => playerDic;

    // Get list of all users as String
    public string GetString(string log = "\n")
    {
        foreach (var player in playerDic) log += player.ToString() + "\n";
        return log;
    }
}
