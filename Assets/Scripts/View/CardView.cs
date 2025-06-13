using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    public int cardId;
    private Sprite frontSprite;
    private Sprite backSprite;

    public Image image;
    [SerializeField] private Transform particleSpawnPoint;

    private ParticleSystem matchParticle;
    private bool isFlipped = false;
    private CardController controller;

    public void Initialize(CardController controllerRef, Sprite backImage)
    {
        controller = controllerRef;
        backSprite = backImage;
        if (image == null)
            image = GetComponent<Image>();
        image.sprite = backSprite;
    }

    public void SetCard(int id, Sprite frontSprite)
    {
        cardId = id;
        this.frontSprite = frontSprite;
    }

    public void SetParticleSystem(GameObject particlePrefab)
    {
        if (particlePrefab != null && particleSpawnPoint != null)
        {
            GameObject instance = Instantiate(particlePrefab, particleSpawnPoint.position, Quaternion.identity, particleSpawnPoint);

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

    public void OnPointerClick(PointerEventData e)
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
            image.sprite = frontSprite;
            LeanTween.rotateY(gameObject, 0f, 0.25f).setOnComplete(() => onComplete?.Invoke());
        });
    }

    public void Hide(System.Action onComplete = null)
    {
        if (!isFlipped) return;
        isFlipped = false;

        
        LeanTween.rotateY(gameObject, 0f, 0.25f).setOnComplete(() =>
        {
            image.sprite = backSprite;
            LeanTween.rotateY(gameObject, 90f, 0.25f).setOnComplete(() => onComplete?.Invoke());
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
