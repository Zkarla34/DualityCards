using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GenerateCards : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Texture2D[] cardTextures;
    [SerializeField] private Transform cardParent;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float showDuration = 2f;

    [SerializeField] private MenuWin menuWin;
    [SerializeField] private ScoreManager scoreManager;

    private List<Card> cards = new List<Card>();
    private Card cardShown;
    private int numFoundCouples;
    public bool show;

    void Awake()
    {
        CreateCards();
    }

    public void CreateCards()
    {
        float factor = 5.0f / width;
        int cardID = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 positionTem = new Vector3(j * factor, 0, i * factor);
                GameObject cardTemp = Instantiate(cardPrefab, positionTem, 
                    Quaternion.Euler(0,180,0), cardParent);

                cardTemp.transform.localScale *= factor;

                Card card = cardTemp.GetComponent<Card>();
                card.positionOriginal = positionTem;
                card.idCard = cardID;
                cards.Add(card);
                cardID++;
            }
        }
        AssignTexturesToCards();
        ShuffleCards();
    }

    void AssignTexturesToCards()
    {
        int texturePairs = cardTextures.Length;

        for(int i=0; i <cards.Count; i++)
        {
            int textureIndex = i / 2;
            cards[i].SetTexture(cardTextures[textureIndex]);
            cards[i].idCard = textureIndex;
        }
    }

    void ShuffleCards()
    {
        for(int i = 0; i< cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            SwapCardPositions(cards[i], cards[randomIndex]);
        }
    }

    private void SwapCardPositions(Card cardA, Card cardB)
    {
        Vector3 tempPosition = cardA.positionOriginal;
        cardA.positionOriginal = cardB.positionOriginal;
        cardB.positionOriginal = tempPosition;

        cardA.transform.position = cardA.positionOriginal;
        cardB.transform.position = cardB.positionOriginal;
    }
    public void SelectCard(Card selectedCard)
    {
        if(cardShown == null)
        {
            cardShown = selectedCard;
        }
        else
        {
            if (scoreManager.CompareCards(selectedCard.gameObject, cardShown.gameObject))
            {
                HandleMatch();
            }
            else
            {
                StartCoroutine(HideCardsAfterDelay(selectedCard, cardShown));
            }
            cardShown = null;
        }
    }

    private void HandleMatch()
    {
        numFoundCouples++;
        if(numFoundCouples == cards.Count / 2)
        {
            menuWin.WinShow();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator HideCardsAfterDelay(Card firstCard, Card secondCard)
    {
        yield return new WaitForSeconds(showDuration);
        firstCard.HideCardInstantly();
        secondCard.HideCardInstantly();
    }
}
