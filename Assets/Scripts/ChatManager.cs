using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mirror;
using Org.BouncyCastle.Crypto.Macs;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake ()
    {
        instance = this;
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
}