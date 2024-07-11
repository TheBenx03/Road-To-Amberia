using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

public class ChatManager : NetworkBehaviour
{
    public int maxMessages = 25;
    public static string localPlayerName = "Test";
    public static string localPlayerColor = "#FFFFFF";

    public GameObject chatPanel, message, clientPlayer;
    public TMP_InputField chatBox;

    [SerializeField] List<GameObject> messageList = new();
    internal static readonly Dictionary<NetworkConnectionToClient, string> connNames = new();

    public static ChatManager instance;
    private NetworkMatch networkMatch;

    void Awake ()
    {
        instance = this;
        networkMatch = GetComponent<NetworkMatch>();
    }

    void Update () 
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CmdSend(localPlayerName, localPlayerColor, chatBox.text);
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                chatBox.ActivateInputField();
        }
    }

    public void ExitButtonOnClick()
    {
        NetworkManager.singleton.StopHost();
        ClearMessageList();
        messageList.Clear();
    }

    public void ClearMessageList()
    {
        foreach (GameObject child in messageList)
            Destroy(child);
    }

    public override void OnStartServer()
    {
        connNames.Clear();
        networkMatch.matchId = GetRandomMatchID();
    }

    [ClientRpc]
    public void SendMessageToChat(string nick, string color, string msg)
    {
        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0]);
            messageList.Remove(messageList[0]);
        }
        GameObject newInstance = Instantiate(message, chatPanel.transform);
        newInstance.GetComponent<Message>().CreateMessage(msg, nick, color);
        messageList.Add(newInstance);
    }

    [Command(requiresAuthority = false)]
    void CmdSend(string nick, string color, string msg, NetworkConnectionToClient sender = null)
    {
        if (!connNames.ContainsKey(sender))
            connNames.Add(sender, sender.identity.GetComponent<Player>().playerName);
        if (!string.IsNullOrWhiteSpace(msg))
            SendMessageToChat(nick, color, msg);
    }

    public static Guid GetRandomMatchID() {
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