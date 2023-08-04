using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GenerateCards : MonoBehaviour
{
    public GameObject cardPrefab; 
    public Texture2D[] cardTexture;
    public Transform cardParent;
    private List<GameObject> cards = new List<GameObject>();
    public Card cardShown;
    public MenuWin textWin;
    public ScoreManager scoreManager;

    public int width;
    public int height;
    public bool show;
    public bool winShow;
    public int counter;
    public int numFoundCouples;
    public float showDuration = 2f;


    void Awake()
    {
        CreateCards();
    }

    public void CreateCards()
    {
        int cont = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float factor = 5.0f/ width;

                Vector3 positionTem = new Vector3(j*factor, 0, i*factor);

                GameObject cardTemp = Instantiate(cardPrefab, positionTem, 
                    Quaternion.Euler(new Vector3(0,180,0)));

                cardTemp.transform.localScale *= factor;

                cards.Add(cardTemp);
                cardTemp.GetComponent<Card>().positionOriginal = positionTem;
                cardTemp.GetComponent<Card>().idCard = cont;
                cardTemp.transform.parent = cardParent;
                cont++;
            }
        }
        AssignTexturesCards();
        ShuffleCards();
    }

    void AssignTexturesCards()
    {

        for(int i=0; i <cards.Count; i++)
        {
            cards[i].GetComponent<Card>().ChangeTexture(cardTexture[(i)/2]);
            cards[i].GetComponent<Card>().idCard = i / 2;
        }
    }

    void ShuffleCards()
    {
        int shuffleRandom;

        for(int i = 0; i< cards.Count; i++)
        {
            shuffleRandom = Random.Range(i, cards.Count);

            cards[i].transform.position = cards[shuffleRandom].transform.position;
            cards[shuffleRandom].transform.position = cards[i].GetComponent<Card>().positionOriginal;

            cards[i].GetComponent<Card>().positionOriginal = cards[i].transform.position;
            cards[shuffleRandom].GetComponent<Card>().positionOriginal = cards[shuffleRandom].transform.position;
        }
    }

    public void SelectCard(Card _card)
    {
        if(cardShown == null)
        {
            cardShown = _card;
        }
        else
        {
            if (scoreManager.CompareCards(_card.gameObject, cardShown.gameObject))
            {
                numFoundCouples++;
                if(numFoundCouples == cards.Count / 2)
                {
                    gameObject.SetActive(false);
                    textWin.GetComponent<MenuWin>().WinShow();
                }
            }
            else
            {
                _card.HideCard();
                cardShown.HideCard();
            }
            cardShown = null;
        }
    }
}

