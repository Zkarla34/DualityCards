using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveLoadBest : MonoBehaviour
{
    public PlayerInfo highScore;
    private string filePath;


    private void Start()
    {
        filePath = Application.persistentDataPath + "/savefile.json";
        Debug.Log(highScore.namePlayer);
        Debug.Log(highScore.scorePlayer);
        LoadHighScore();
    }

    [ContextMenu("LoadHighScore")]
    public void LoadHighScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScore = JsonUtility.FromJson<PlayerInfo>(json);
        }
        else
        {
            highScore = new PlayerInfo();
        }
    }

    private void SaveHighScore()
    {
        string json = JsonUtility.ToJson(highScore);
        File.WriteAllText(filePath, json);

    }

    public void UpdateHighScore(string playerName, int newScore)
    {
        if (newScore > highScore.scorePlayer)
        {
            highScore.namePlayer = playerName;
            highScore.scorePlayer = newScore;
            SaveHighScore();
        }
    }

    public string GetHighScorePlayerName()
    {
        return highScore.namePlayer;
    }

    public int GetHighScore()
    {
        return highScore.scorePlayer;
    }
}
