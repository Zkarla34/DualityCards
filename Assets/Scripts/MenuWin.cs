using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MenuWin : MonoBehaviour
{

    public GameObject menu;
    public Text textWin;
    public int counterText;
    public Text namePlayer;
    public Text counterNum;

    public bool menuActive;

    private void Start()
    {
        counterNum.text = "" + counterText.ToString();
        GameManager.Instance.score = counterText;
        menu.SetActive(false);
        menuActive = false;
    }
    public void WinShow()
    {
        namePlayer.text = GameManager.Instance.playerName;
        counterText = GameManager.Instance.score;
        menu.SetActive(true);
        menuActive = true;
    }

    public void WinHide()
    {
        menu.SetActive(false);
        menuActive = false;
    }

}
