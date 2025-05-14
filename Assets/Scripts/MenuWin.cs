using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuWin : MonoBehaviour
{

    public GameObject menu;
    public GameObject score;
    public Text textWin;
    public Text namePlayer;

    public bool menuActive;

    private void Start()
    {
        menu.SetActive(false);
        menuActive = false;
    }
    public void WinShow()
    {
        namePlayer.text = GameManager.Instance.playerName + " your score is:  " + ScoreManager.Instance.counter;
        menu.SetActive(true);
        menuActive = true;
        score.SetActive(false);
    }
    public void WinHide()
    {
        menu.SetActive(false);
        menuActive = false;
    }
}