using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] matchParticlePrefabs;
    [SerializeField] private MenuWin menuWin;
    [SerializeField] private ScoreManager scoreManager;
    private LevelModel levelModel;

    private List<CardView> cards = new();
    private CardView showCard = null;

    [SerializeField] private RectTransform cardsAreaUI;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Sprite backSprite;



    private int currentLevel = 1;
    private bool allowInteraction = true;

    public bool CanShowCard() => allowInteraction;
    public void DisableInteraction() => allowInteraction = false;
    public void EnableInteraction() => allowInteraction = true;

    private void Awake()
    {
        levelModel = new LevelModel();
        StartCoroutine(DelayGenerateCards(1f));
    }

    private void ClearPreviousCards()
    {
        foreach (Transform child in cardsAreaUI)
        {
            Destroy(child.gameObject);
        }
        cards.Clear();
        showCard = null;
    }


    public void OnCardSelected(CardView selected)
    {
        if (!allowInteraction || selected == showCard || selected == null) return;

        DisableInteraction();
        selected.Show(() =>
        {
            if (showCard == null)
            {
                showCard = selected;
                EnableInteraction();
            }
            else
            {
                if (scoreManager.CompareCards(selected, showCard))
                {
                    selected.PlayMatchParticles();
                    showCard.PlayMatchParticles();

                    cards.Remove(selected);
                    cards.Remove(showCard);

                   
                    selected.DeactivateAfterMatch();
                    showCard.DeactivateAfterMatch();

                    showCard = null;

                    AudioManager.Instance.PlayMatchSound(currentLevel - 1);

                    if (cards.Count == 0)
                    {
                        Invoke(nameof(LoadNextLevel), 0.5f);
                    }
                    else
                    {
                        EnableInteraction();
                    }
                }
                else
                {
                    LeanTween.delayedCall(1f, () =>
                    {
                        selected.Hide();
                        showCard.Hide();
                        showCard = null;
                        EnableInteraction();
                        AudioManager.Instance.PlayWrongMatch();
                    });
                }
            }
        });
    }
    

    private void GenerateCards()
    {
        int pairCount = 2 + (currentLevel - 1);
        int totalCards = pairCount * 2;

        int gridCols = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
        int gridRows = Mathf.CeilToInt((float)totalCards / gridCols);

        float scale = Mathf.Clamp(1f - currentLevel * 0.05f, 0.5f, 1f);

        List<(int id, Sprite sprite)> selected = new();
        for (int i = 0; i < pairCount; i++)
        {
            selected.Add((i, sprites[i]));
            selected.Add((i, sprites[i]));
        }

        // Shuffle
        for (int i = 0; i < selected.Count; i++)
        {
            var tmp = selected[i];
            int rand = Random.Range(i, selected.Count);
            selected[i] = selected[rand];
            selected[rand] = tmp;
        }

        // Posicionar en grilla manual o usar GridLayoutGroup
        for (int i = 0; i < totalCards; i++)
        {
            GameObject cardGO = Instantiate(cardUIPrefab, cardsAreaUI);
            cardGO.transform.localScale = Vector3.one * scale;

            CardView cardUI = cardGO.GetComponent<CardView>();
            cardUI.Initialize(this, backSprite);
            cardUI.SetCard(selected[i].id, selected[i].sprite);

            int particleIndex = Mathf.Clamp(currentLevel - 1, 0, matchParticlePrefabs.Length - 1);
            cardUI.SetParticleSystem(matchParticlePrefabs[particleIndex]);

            cards.Add(cardUI);
        }

        EnableInteraction();
    }

    private void LoadNextLevel()
    {
        int nextLevel = currentLevel + 1;
        int nextPairCount = levelModel.GetPairCount(nextLevel);

        // Verificamos si hay suficientes sprites para el siguiente nivel
        if (nextPairCount * 2 > sprites.Length)
        {
            // No hay más sprites disponibles: juego terminado
            menuWin.WinShow();
        }
        else
        {
            currentLevel = nextLevel;
            levelModel.currentLevel = currentLevel;
            StartCoroutine(DelayGenerateCards(0.5f));
        }
    }


    private IEnumerator DelayGenerateCards(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearPreviousCards();
        GenerateCards();
    }
}