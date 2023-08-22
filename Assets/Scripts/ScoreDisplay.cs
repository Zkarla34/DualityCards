using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshPro playerNameText;
    public TextMeshPro highScoreText;
    public SaveLoadBest bestScore;

    private void Start()
    {
        playerNameText.text = bestScore.GetHighScorePlayerName();
        highScoreText.text = "" + bestScore.GetHighScore();
    }
}
