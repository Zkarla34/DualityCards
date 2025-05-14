using TMPro;
using UnityEngine;

public class HistoryView : MonoBehaviour
{
    public TMP_Text dialogueTextHistory;
    public GameObject buttonSkip;


    public void ClearDialogue()
    {
        dialogueTextHistory.text = "";
    }

    public void AppendLetter(char letter)
    {
        dialogueTextHistory.text += letter;
    }

    public void ShowSkipButton()
    {
        LeanTween.scale(buttonSkip, Vector2.one, 1f).setEase(LeanTweenType.easeInOutBack);
    }

    public void ShowFullDialogue(string fullText)
    {
        dialogueTextHistory.text = fullText;
    }
}
