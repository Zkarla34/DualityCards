using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Texture2D[] textures;
    [SerializeField] private Texture2D backTexture;
    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject[] matchParticlePrefabs;
    [SerializeField] private MenuWin menuWin;
    [SerializeField] private ScoreManager scoreManager;
    private LevelModel levelModel;

    private List<CardView> cards = new();
    private CardView showCard = null;

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
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
        cards.Clear();
        showCard = null;
    }

    private void GenerateCards()
    {
        int pairCount = 2 + (currentLevel - 1);
        int totalCards = pairCount * 2;

        // Cálculo dinámico del tamaño de la grilla
        int gridCols = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
        int gridRows = Mathf.CeilToInt((float)totalCards / gridCols);

        // Disminuye el tamaño de las cartas a medida que hay más
        float scaleFactor = Mathf.Clamp(1f - (currentLevel * 0.05f), 0.5f, 1f);
        float spacing = 2.5f * scaleFactor;

        float offsetX = (gridCols - 1) * spacing * 0.5f;
        float offsetZ = (gridRows - 1) * spacing * 0.5f;

        List<(int id, Texture2D tex)> selected = new();

        for (int i = 0; i < pairCount; i++)
        {
            selected.Add((i, textures[i]));
            selected.Add((i, textures[i]));
        }

        // Mezclar
        for (int i = 0; i < selected.Count; i++)
        {
            var temp = selected[i];
            int rand = Random.Range(i, selected.Count);
            selected[i] = selected[rand];
            selected[rand] = temp;
        }

        for (int i = 0; i < selected.Count; i++)
        {
            int row = i / gridCols;
            int col = i % gridCols;

            Vector3 pos = new Vector3(col * spacing - offsetX, 0f, - row * spacing + offsetZ);

            GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, cardParent);

            // Escalado dinámico según nivel
            go.transform.localScale = Vector3.zero;

            // Aparecer animado
            LeanTween.scale(go, Vector3.one * scaleFactor, 0.3f)
                     .setEaseOutBack()
                     .setDelay(i * 0.05f);


            CardView card = go.GetComponent<CardView>();
            card.Initialize(this, backTexture);
            card.SetCard(selected[i].id, selected[i].tex);

            int particleIndex = Mathf.Clamp(currentLevel - 1, 0, matchParticlePrefabs.Length - 1);
            card.SetParticleSystem(matchParticlePrefabs[particleIndex]);

            cards.Add(card);
        }

        EnableInteraction(); 
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

    private void LoadNextLevel()
    {
        int nextLevel = currentLevel + 1;
        int nextPairCount = levelModel.GetPairCount(nextLevel);

        // Verificamos si hay suficientes texturas para el siguiente nivel
        if (nextPairCount * 2 > textures.Length)
        {
            // No hay más texturas disponibles: juego terminado
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