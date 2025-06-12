using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerTransitions : MonoBehaviour
{
    [SerializeField] private MenuViewTransitions menuViewTransitions;
    [SerializeField] private SceneTransition sceneTransition;

    private void Start()
    {
        menuViewTransitions.InitView();
        menuViewTransitions.ShowContent();
    }

    public void OnStartGameButtonPressed()
    {
        sceneTransition.FadeToScene("History");
    }

    public void OnOpenNicknamePanel()
    {
        menuViewTransitions.ShowNickNamePanel();
    }

    public void OnCloseNicknamePanel()
    {
        menuViewTransitions.HideNickNamePanel();
    }
}
