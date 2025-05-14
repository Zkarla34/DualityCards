using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class HighScoreManager : MonoBehaviour
{
    private string filePath;
    private HighScoreData highScoreData;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/highscores.json";
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScoreData = JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {
            highScoreData = new HighScoreData();
        }
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(highScoreData, true);
        File.WriteAllText(filePath, json);
    }

    public void AddPlayerScore(string playerName, int score)
    {
        PlayerModel newPlayer = new PlayerModel(playerName, score);
        highScoreData.players.Add(newPlayer);
        SaveData();
    }

    public PlayerModel GetBestPlayer()
    {
        if (highScoreData.players.Count == 0)
        {
            return null;
        }
        return highScoreData.players.OrderByDescending(p => p.Score).First();
    }

    public List<PlayerModel> GetAllPlayers()
    {
        return highScoreData.players;
    }
}
