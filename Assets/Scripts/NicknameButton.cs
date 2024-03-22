using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NicknameButton : MonoBehaviour
{
    public GameObject nickWindow;
    public Canvas canvasArea;
    Button open;
    Button apply;
    GameObject newWindow;

    void Start()
    {
        open = GetComponent<Button>();
        open.onClick.AddListener(OpenNickWindow);
    }

    public void OpenNickWindow()
    {
        newWindow = Instantiate(nickWindow, canvasArea.transform);
        TMP_InputField[] inputFieldArray = newWindow.GetComponentsInChildren<TMP_InputField>();
        inputFieldArray[1].text = CanvasChatRoom.nickColour;
        inputFieldArray[0].text = CanvasChatRoom.nickName;
        apply = newWindow.GetComponentInChildren<Button>();
        apply.onClick.AddListener(ApplyNickWindow);
    }
    public void ApplyNickWindow()
    {
        TMP_InputField[] inputFieldArray = newWindow.GetComponentsInChildren<TMP_InputField>();
        CanvasChatRoom.nickColour = inputFieldArray[1].text;
        CanvasChatRoom.nickName = inputFieldArray[0].text;
        Destroy(newWindow);
    }
}