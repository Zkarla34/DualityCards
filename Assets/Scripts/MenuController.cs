using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class MenuController : MonoBehaviour
{
    public MenuTransitionsUI menuTransitionsUI;
    public GameManager gameManager;

    [SerializeField] private InputField inputName;
    [SerializeField] private Image imageOk;
    [SerializeField] private GameObject butonSave;
    [SerializeField] private GameObject panelNickName;
    [SerializeField] private ParticleSystem particleEffectPanelNickName;

    internal string playerName;

    private void Start()
    {
        panelNickName.SetActive(false);
        imageOk.color = Color.red;
        butonSave.SetActive(false);
    }

    private void Update()
    {
        if (inputName.text.Length < 3) 
        {
            imageOk.color = Color.red;
            butonSave.SetActive(false);
        }
        if(inputName.text.Length >= 3)
        {
            imageOk.color = Color.green;
            butonSave.SetActive(true);
        }
    }

    public void OnPlayButtonClicked()
    {
        ShowPanelNickName();
    }

    //Show panel nickname
    private void ShowPanelNickName()
    {
        panelNickName.SetActive(true);
        particleEffectPanelNickName.Play();
        menuTransitionsUI.ShowPanelImageNickName();
    }

    //Click on button save in the panel Nickname
    public void GoPlay()
    {
        string playerName = inputName.text;
        GameManager.Instance.playerName = playerName;
        particleEffectPanelNickName.Play();
        menuTransitionsUI.HidePanelImageNickName();
        StartCoroutine(gameManager.NextScene(0.7f,1));
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
