using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    private SceneTransition sceneTransition;

    public void OnResetButtonClicked()
    {
        sceneTransition.FadeToScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuButtonClicked()
    {
        sceneTransition.FadeToScene("Menu"); 
    }
}
