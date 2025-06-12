using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration;

    public static SceneTransition Instance { get; private set; }

    private bool comingFromFade = false; 

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        fadeImage.gameObject.SetActive(false);
        fadeImage.color = new Color(0, 0, 0, 0); 
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
    public void FadeToScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0); 
        fadeImage.DOFade(1, fadeDuration).OnComplete(() =>
        {
            comingFromFade = true;
            SceneManager.LoadScene(sceneName);
        });
    }

    /// <summary>
    /// Al llegar a una nueva escena
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (comingFromFade)
        {
            fadeImage.color = new Color(0, 0, 0, 1); 
            fadeImage.gameObject.SetActive(true);
            FadeIn();
            comingFromFade = false; 
        }
        else
        {
            // No hacer fade-in si no venimos de transición
            fadeImage.gameObject.SetActive(false); 
        }
    }

    /// <summary>
    /// Hace fade-in desde negro a transparente
    /// </summary>
    private void FadeIn()
    {
        fadeImage.DOFade(0, fadeDuration).OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false);
        });
    }
}
