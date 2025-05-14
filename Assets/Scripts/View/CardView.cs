using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public int cardId;
    private Texture2D frontTexture;
    private Texture2D backTexture;

    [SerializeField] private Renderer frontRenderer;
    [SerializeField] private Transform particleSpawnPoint;

    private ParticleSystem matchParticle;
    private bool isFlipped = false;
    private CardController controller;

    public void Initialize(CardController controllerRef, Texture2D backTex)
    {
        controller = controllerRef;
        backTexture = backTex;
        frontRenderer.material.mainTexture = backTexture;
    }

    public void SetCard(int id, Texture2D tex)
    {
        cardId = id;
        frontTexture = tex;
    }

    public void SetParticleSystem(GameObject particlePrefab)
    {
        if (particlePrefab != null && particleSpawnPoint != null)
        {
            GameObject instance = Instantiate(particlePrefab, particleSpawnPoint.position, Quaternion.identity, transform);

            // Buscar también en hijos
            matchParticle = instance.GetComponent<ParticleSystem>();

            if (matchParticle == null)
            {
                Debug.LogWarning("No se encontró ParticleSystem en el prefab ni en sus hijos en carta " + cardId);
            }
            else
            {
                Debug.Log("Sistema de partículas asignado correctamente a la carta " + cardId);

            }
        }
    }

    private void OnMouseDown()
    {
        if (controller != null && controller.CanShowCard())
        {
            controller.OnCardSelected(this);
        }
    }

    public void Show(System.Action onComplete = null)
    {
        if (isFlipped) return;
        isFlipped = true;

        LeanTween.rotateY(gameObject, 90f, 0.25f).setOnComplete(() =>
        {
            frontRenderer.material.mainTexture = frontTexture;
            LeanTween.rotateY(gameObject, 180f, 0.25f).setOnComplete(() => onComplete?.Invoke());
        });
    }

    public void Hide(System.Action onComplete = null)
    {
        if (!isFlipped) return;
        isFlipped = false;

        LeanTween.rotateY(gameObject, 90f, 0.25f).setOnComplete(() =>
        {
            frontRenderer.material.mainTexture = backTexture;
            LeanTween.rotateY(gameObject, 0f, 0.25f).setOnComplete(() => onComplete?.Invoke());
        });
    }

    public void PlayMatchParticles()
    {
        if (matchParticle != null)
        {
            Debug.Log("Reproduciendo partículas para carta " + cardId);
            matchParticle.Play();
        }
    }

    public int GetCardId() => cardId;

    public void DeactivateAfterMatch()
    {
        LeanTween.delayedCall(0.5f, () =>
        {
            gameObject.SetActive(false);
        });
    }
}
