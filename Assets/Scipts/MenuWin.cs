using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWin : MonoBehaviour
{
    public GameObject menu;
    public Text textWin;

    public bool menuActive;

    private void Start()
    {
        menu.SetActive(false);
        menuActive = false;
    }
    public void WinShow()
    {
        menu.SetActive(true);
        menuActive = true;
    }

    public void WinHide()
    {
        menu.SetActive(false);
        menuActive = false;
    }

}
