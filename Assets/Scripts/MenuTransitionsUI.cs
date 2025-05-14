using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MenuTransitionsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textsPanel;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Image image;
    [SerializeField] private GameObject panelImageNickName;
    public float animationDuration;
    Color transparentWhite = new Color(1, 1, 1, 0);

    private void Start()
    {
        image.color = transparentWhite;
        textsPanel.color = transparentWhite;

        panelImageNickName.transform.localScale = Vector3.zero;

        foreach (GameObject button in buttons)
        {
            button.transform.localScale = Vector3.zero;
        }
        ShowContent();
    }

    private void ShowContent()
    {
        Invoke(nameof(ShowImage), 0.5f);
        Invoke(nameof(ChangeColorText), 1f);
        Invoke(nameof(AnimateButtons), 1.5f);
    }

    private void ChangeColorText()
    {
        LeanTween.value(gameObject, 0f, 1f, animationDuration).setOnUpdate((float val) =>
        {
            Color newColor = new Color(1, 1, 1, val);
            textsPanel.color = newColor;
        });
        
    }
    private void ShowImage()
    {
        LeanTween.value(gameObject, 0f,1f, animationDuration).setOnUpdate((float val) =>
        {
            image.color = new Color(1,1,1,val);
        });
    }

    private void AnimateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            LeanTween.scale(buttons[index], Vector3.one, animationDuration).setEase(LeanTweenType.easeOutCubic).setDelay(i * 0.1f);
        }
    }

    public void ShowPanelImageNickName()
    {
        LeanTween.scale(panelImageNickName, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeInOutBack);
    }

    public void HidePanelImageNickName()
    {
        LeanTween.scale(panelImageNickName, Vector3.zero, 0.5f)
           .setEase(LeanTweenType.easeInBack);
    }
}