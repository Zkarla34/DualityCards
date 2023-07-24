using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private GameObject chargeLevel;
    private GameObject chargeCredits;
    private GameObject chargeMenu;
    //Go to level
    public void GoPlay()
    {
      SceneManager.LoadScene(1);
    }
    //Go to credits
    public void GoCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
