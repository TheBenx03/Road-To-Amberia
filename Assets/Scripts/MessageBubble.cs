using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CanvasChatRoom : MonoBehaviour
{
    public int maxMessages = 25;
    public static string nickName;
    public static string nickColour;

    public GameObject chatPanel, messageBubble;
    public TMP_InputField chatBox;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    //public TMP_Text[] PackagedMessage;

    void Start()
    {
        nickName = LoginScreenHandler.nickName + ": ";
        nickColour = LoginScreenHandler.nickColour;
    }

    void Update () 
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatBox.text, Message.MessageType.playerMessage);
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                chatBox.ActivateInputField();
        }
    }

    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        if(messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].messageObject);
            messageList.Remove(messageList[0]);
        }
        Message newMessage = new Message();
        GameObject newText = Instantiate(messageBubble, chatPanel.transform);
        newMessage.SetVariables(text, nickName, nickColour, newText, newText.GetComponentsInChildren<TextMeshProUGUI>());
        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string textMessage;
    public string textNickName;
    public string textNickColor;

    public GameObject messageObject;
    public TMP_Text[] messageArray;
    public MessageType messageType;

    public void SetVariables(string msg, string nick, string color, GameObject msgObject, TMP_Text[] msgArray)
    {
        textMessage = msg;
        textNickName = nick;
        textNickColor = color;
        messageObject = msgObject;
        messageArray = msgArray;
        ColorUtility.TryParseHtmlString(textNickColor, out Color newCol);
        messageArray[1].text = textMessage;
        messageArray[0].text = textNickName;
        messageArray[0].color = newCol;
    }

    public enum MessageType
    {
        playerMessage,
        info
    }
}