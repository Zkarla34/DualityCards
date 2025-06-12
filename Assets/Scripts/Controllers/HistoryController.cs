using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private HistoryView historyView;
    private HistoryModel historyModel;

    private Coroutine typingCoroutine;
    private bool isTyping;

    private void Start()
    {
        historyModel = new HistoryModel();

        historyView.ClearDialogue();
        historyView.ShowSkipButton();
        isTyping = true;
        typingCoroutine = StartCoroutine(TypingMessage());
    }

    IEnumerator TypingMessage()
    {
        if(isTyping )
        {
            foreach (char letter in historyModel.dialogueHistory)
            {
                historyView.AppendLetter(letter);
                yield return new WaitForSeconds(historyModel.wordSpeed);
            }
        }
        
    }

    public void ButtonShowMessageFast()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        isTyping = false;
        historyView.ShowFullDialogue(historyModel.dialogueHistory);
    }
}
