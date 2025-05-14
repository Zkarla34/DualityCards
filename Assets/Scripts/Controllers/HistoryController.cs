using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private HistoryView historyView;
    private HistoryModel historyModel;
    private GameManager gameManager;

    private Coroutine typingCoroutine;

    private void Start()
    {
        gameManager = GameManager.Instance;

        historyModel = new HistoryModel();

        historyView.ClearDialogue();
        historyView.ShowSkipButton();

        typingCoroutine = StartCoroutine(TypingMessage());
    }

    IEnumerator TypingMessage()
    {
        foreach (char letter in historyModel.dialogueHistory)
        {
            historyView.AppendLetter(letter);
            yield return new WaitForSeconds(historyModel.wordSpeed);
        }
    }

    public void ButtonShowMessageFast()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        historyView.ShowFullDialogue(historyModel.dialogueHistory);

        StartCoroutine(gameManager.NextScene(0.7f, 2));
    }
}
