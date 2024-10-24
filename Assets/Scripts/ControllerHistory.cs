using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerHistory : MonoBehaviour
{
    private GameManager gameManager;

    public TMP_Text dialogueTextHistory;
    public GameObject buttonSkip;

    private string dialogueHistory = "hola hola hola hola hola hola";
    public float wordSpeed;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        buttonSkip.transform.localScale = Vector3.zero;
        StartCoroutine(TypingMessage());
    }

    //Write words Assigment Message
    IEnumerator TypingMessage()
    {
        LeanTween.scale(buttonSkip, Vector2.one, 1f).setEase(LeanTweenType.easeInOutBack);
        int charIndex = 0;
        foreach (char letter in dialogueHistory)
        {
            dialogueTextHistory.text += letter;
            charIndex++;
            yield return new WaitForSeconds(wordSpeed);
        }
        
    }

    public void ButtonShowMessageFast()
    {
        StopAllCoroutines();
        dialogueTextHistory.text = dialogueHistory;
        StartCoroutine(gameManager.NextScene(0.7f, 2));
    }
}
