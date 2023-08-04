using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int counter;
    public Text counterText;


    private void Start()
    {
        counterText.text = "" + counter.ToString();
        GameManager.Instance.score = counter;
    }
    public bool CompareCards(GameObject cardOne, GameObject cardTwo)
    {
        bool result;
        if (cardOne.GetComponent<Card>().idCard ==
             cardTwo.GetComponent<Card>().idCard)
        {
            AddScore();
            result = true;
        }
        else
        {
            SubtractScore();
            result = false;
        }
        return result;
    }


    public void AddScore()
    {
        counter = counter + 10;
        counterText.text = "" + counter;
    }
    public void SubtractScore()
    {
        counter = counter - 10;
        if (counter <= 0)
        {
            counter = 0;
        }
        counterText.text = "" + counter;
    }
}
