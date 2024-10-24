using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public SaveLoadBest playerInfo;

    // Llamada cuando se alcanza un nuevo puntaje alto
    public void NewHighScoreAchieved(string playerName, int newScore)
    {
        playerInfo.UpdateHighScore(playerName, newScore);
    }

    public IEnumerator NextScene(float time, int scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

}