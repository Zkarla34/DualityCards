using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModel 
{
    public int currentLevel = 1;

    public int GetPairCount(int level = -1)
    {
        int lvl = (level == -1) ? currentLevel : level;
        return Mathf.Min(2 + lvl - 1, 10); 
    }
}
