using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsViewTransitions : MonoBehaviour
{
    [Header("Visual References")]
    public TMP_Text[] textsCredits;
    public GameObject buttonBack;

    [Header("Animation Duration")]
    public float animationDuration;

    private Color transparentWhite = new Color(1, 1, 1, 0);

    public void InitView()
    {
        buttonBack.transform.localScale = Vector3.zero;
        foreach (TMP_Text text in textsCredits)
        {
            text.color = transparentWhite;
        }
    }

    public void ShowContent()
    {
        
        Invoke(nameof(ShowText), 0.5f);
        Invoke(nameof(ShowButtons), 1.5f);
    }

    private void ShowText()
    {

        for(int i = 0; i<textsCredits.Length; i++ )
        {
            int index = i;
            LeanTween.value(gameObject, 0f, 1f, animationDuration).setOnUpdate((float val) =>
            {
                textsCredits[index].color = new Color(1, 1, 1, val);
            }).setEase(LeanTweenType.easeInOutSine).setDelay(i * 0.3f); 
        } 
    }


    private void ShowButtons()
    {
        LeanTween.scale(buttonBack, Vector3.one, animationDuration)
            .setEase(LeanTweenType.easeOutCubic);
    }
}
