using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies;
using UnityEngine;

[Serializable]
public class Message : MonoBehaviour
{
    public TMP_Text message;
    public TMP_Text nickname;

    public void CreateMessage(string msg, string nick, string clr)
    {
        message.text = msg;
        nickname.text = nick;
        ColorUtility.TryParseHtmlString(clr, out Color newCol);
        nickname.color = newCol;
    }

    public void CreateMessage(string nick, string clr)
    {
        nickname.text = nick;
        ColorUtility.TryParseHtmlString(clr, out Color newCol);
        nickname.color = newCol;
    }    
}