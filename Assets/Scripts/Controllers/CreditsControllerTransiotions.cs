using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsControllerTransiotions : MonoBehaviour
{
    [SerializeField] private CreditsViewTransitions creditsViewTransitions;

    private void Start()
    {
        creditsViewTransitions.InitView();
        creditsViewTransitions.ShowContent();
    }

    public void BackToMenu()
    {
        SceneTransition.Instance.FadeToScene("Menu");
    }
}
