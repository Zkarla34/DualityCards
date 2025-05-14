using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int counter;
    public Text counterText;

    private void Awake()
    {
        // Singleton seguro
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        counterText.text = counter.ToString();
    }

    /// <summary>
    /// Compara dos cartas por su ID. Si coinciden, suma puntos. Si no, los resta.
    /// </summary>
    public bool CompareCards(CardView cardOne, CardView cardTwo)
    {
        if (cardOne == null || cardTwo == null)
        {
            Debug.LogWarning("Una o ambas cartas son nulas.");
            return false;
        }

        if (cardOne.cardId == cardTwo.cardId)
        {
            AddScore();
            return true;
        }
        else
        {
            SubtractScore();
            return false;
        }
    }

    public void AddScore()
    {
        counter += 10;
        UpdateCounterText();
    }

    public void SubtractScore()
    {
        counter -= 10;
        if (counter < 0) counter = 0;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = counter.ToString();
    }
}