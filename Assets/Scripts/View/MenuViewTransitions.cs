using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuViewTransitions : MonoBehaviour
{
    [Header("Visual References")]
    public TMP_Text textPanel;
    public GameObject[] buttons;
    public Image image;
    public GameObject panelNickName;


    [Header("Animation Duration")]
    public float animationDuration;

    private Color transparentWhite = new Color(1,1, 1, 0);

    public void InitView()
    {
        image.color = transparentWhite;
        textPanel.color = transparentWhite;
        panelNickName.transform.localScale = Vector3.zero;

        foreach(GameObject button in buttons)
        {
            button.transform.localScale = Vector3.zero;
        }
    }

    public void ShowContent()
    {
        Invoke(nameof(ShowImage), 0.5f);
        Invoke(nameof(ShowText), 1f);
        Invoke(nameof(ShowButtons), 1.5f);
    }

    private void ShowText()
    {
        LeanTween.value(gameObject, 0f, 1f, animationDuration).setOnUpdate((float val) =>
        {
            textPanel.color = new Color(1, 1, 1, val);
        });
    }

    private void ShowImage()
    {
        LeanTween.value(gameObject, 0f, 1f, animationDuration).setOnUpdate((float val) =>
        {
            image.color = new Color(1, 1, 1, val);
            
        });
       
    }

    private void ShowButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            LeanTween.scale(buttons[i], Vector3.one, animationDuration)
                .setEase(LeanTweenType.easeOutCubic)
                .setDelay(i * 0.1f);
        }
    }

    public void ShowNickNamePanel()
    {
        LeanTween.scale(panelNickName, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeInOutBack);
    }

    public void HideNickNamePanel()
    {
        LeanTween.scale(panelNickName, Vector3.zero, 0.5f)
            .setEase(LeanTweenType.easeInBack);
    }
}
