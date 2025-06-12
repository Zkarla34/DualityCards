using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int currentScore;

    private HighScoreManager highScoreManager;


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

    private void Start()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();
    }

    public void SaveCurrentPlayerScore()
    {
        if(highScoreManager == null)
        {
            highScoreManager = FindObjectOfType<HighScoreManager>();
        }

        highScoreManager.AddPlayerScore(playerName, currentScore);
    }
}