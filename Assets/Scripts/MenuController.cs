using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    public InputField inputName;
    public Image imageOk;
    public GameObject butonSave;
    public GameObject panelNickName;

    internal string playerName;

    private void Start()
    {
        panelNickName.SetActive(false);
        imageOk.color = Color.red;
        butonSave.SetActive(false);
    }

    private void Update()
    {
        if (inputName.text.Length < 4) 
        {
            imageOk.color = Color.red;
            butonSave.SetActive(false);
        }
        if(inputName.text.Length >= 4)
        {
            imageOk.color = Color.green;
            butonSave.SetActive(true);
        }
    }

    //Show panel nickname
    public void ShowPanelNickName()
    {
        panelNickName.SetActive(true);
    }

    //Click on button save in the panel Nickname
    public void GoPlay()
    {
        string playerName = inputName.text;
        GameManager.Instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }

    //Go to credits
    public void GoCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {

#if UNITY_EDITOR
       // GameManager.Instance.SaveName();
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
