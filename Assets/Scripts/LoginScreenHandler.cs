using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScreenHandler : MonoBehaviour
{
    public Button enter;

    public TMP_InputField inputNickName;
    public TMP_InputField inputNickColour;
    public static string nickName;
    public static string nickColour;

    void Start()
    {
        enter.onClick.AddListener(EnterPressed);
    }

    private void EnterPressed()
    {
        nickName = inputNickName.text;
        nickColour = inputNickColour.text;

        SceneManager.LoadScene("Chatroom");
    }
}
