using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NicknameButton : MonoBehaviour
{
    //OLD: Object used to change name and colour
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
        inputFieldArray[1].text = ChatManager.localPlayerColor;
        inputFieldArray[0].text = ChatManager.localPlayerName;
        apply = newWindow.GetComponentInChildren<Button>();
        apply.onClick.AddListener(ApplyNickWindow);
    }

    public void ApplyNickWindow()
    {
        TMP_InputField[] inputFieldArray = newWindow.GetComponentsInChildren<TMP_InputField>();
        ChatManager.localPlayerColor = inputFieldArray[1].text;
        ChatManager.localPlayerName = inputFieldArray[0].text;
        TMP_Text openText = open.GetComponentInChildren<TMP_Text>();
        UnityEngine.ColorUtility.TryParseHtmlString(inputFieldArray[1].text, out Color newCol);
        openText.color = newCol;
        openText.text = inputFieldArray[0].text;
        Destroy(newWindow);
    }
}