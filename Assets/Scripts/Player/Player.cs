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

    [Header("White Room")]
    [SyncVar] public bool changeMatch = false;
    [SyncVar] public string currentMatch = "";

    NetworkMatch networkMatch;
    Guid netIDGuid;

    void Awake(){
        networkMatch = GetComponent<NetworkMatch>();
    }

    void Update(){
        if (changeMatch){
            networkMatch.matchId = netIDGuid;
            networkMatch.matchId = ToGuid(currentMatch);
            changeMatch = false;
        }
    }

    #region Mirror Overrides
    public override void OnStartServer()
    {
        playerName = (string)connectionToClient.authenticationData;
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
        CmdRegister(playerName);
        StartCoroutine(nameof(JoinMatch));
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        PlayerList.instance.Remove(connectionToClient.connectionId);
    }
    #endregion

    #region Custom Functions
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

    public static string GetRandomMatchID () {
        string _id = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range (0, 36);
            if (random < 26) {
                _id += (char) (random + 65);
            } else {
                _id += (random - 26).ToString ();
            }
        }
        return _id;
    }

    public static Guid ToGuid(string _id){
        Debug.Log ($"Random Match ID: {_id}");
        MD5CryptoServiceProvider provider = new();
        byte[] inputBytes = Encoding.Default.GetBytes (_id);
        byte[] hashBytes = provider.ComputeHash (inputBytes);

        return new Guid (hashBytes);
    }

    [Command(requiresAuthority = false)]
    public void JoinMatch(){
        connectionToClient.identity.GetComponent<NetworkMatch>().matchId = ChatManager.instance.GetComponent<NetworkMatch>().matchId;
    }

    #endregion
}