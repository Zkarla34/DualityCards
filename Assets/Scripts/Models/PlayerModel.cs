using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerModel
{
    public string Name;
    public int Score;

    public PlayerModel(string name, int score)
    {
        Name = name;
        Score = score;
    }   
}
