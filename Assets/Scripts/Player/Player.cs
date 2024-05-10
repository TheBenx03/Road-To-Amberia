using Mirror;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : NetworkBehaviour
{
    [SyncVar] public string playerName;
    [SyncVar] public string playerColor;
    
    //TODO: Make player object interactable (open player profile)

    public override void OnStartServer()
    {
        playerName = (string)connectionToClient.authenticationData;
        CmdRegister(playerName);
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
}