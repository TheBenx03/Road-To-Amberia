using System;
using Mirror;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Security.Cryptography;
using System.Text;

public class Player : NetworkBehaviour
{
    [Header("Player Attributes")]
    [SyncVar] public string playerName;
    [SyncVar] public string playerColor;

    NetworkMatch networkMatch;

    void Awake(){
        networkMatch = GetComponent<NetworkMatch>();
    }

    public override void OnStartServer()
    {
        playerName = (string)connectionToClient.authenticationData;
        CmdRegister(playerName);
        networkMatch.matchId = GetRandomMatchID();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        ChatManager.localPlayerName = playerName;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Set();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        PlayerList.instance.Remove(connectionToClient.connectionId);
    }

    [Command(requiresAuthority = false)]
    private void CmdRegister(string playerName)
    {
        PlayerList.instance.Add(connectionToClient.connectionId, playerName);
        //DEBUG: Log dictionary when entering
        Debug.Log($"Current Player List: {PlayerList.instance.GetString()}");
    }

    public void Set()
    {
        transform.SetParent(PlayerList.instance.transform, false);
        var p = GetComponent<TMP_Text>();
        p.text = playerName;
        ColorUtility.TryParseHtmlString(playerColor, out Color newCol);
        p.color = newCol;
    }

    public static Guid GetRandomMatchID () {
        string _id = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range (0, 36);
            if (random < 26) {
                _id += (char) (random + 65);
            } else {
                _id += (random - 26).ToString ();
            }
        }
        Debug.Log ($"Random Match ID: {_id}");
        MD5CryptoServiceProvider provider = new();
        byte[] inputBytes = Encoding.Default.GetBytes (_id);
        byte[] hashBytes = provider.ComputeHash (inputBytes);

        return new Guid (hashBytes);
    }
}