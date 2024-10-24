using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Texture2D textureCover;
    [SerializeField] private Texture2D textureImage;
    [SerializeField] private GameObject createCard;
    [SerializeField] private float hideDelay;

    private MeshRenderer meshRenderer;
    private GenerateCards generateCards;

    public int idCard { get; set; }
    public bool isShowing { get; private set; }

    public Vector3 positionOriginal;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        generateCards = FindObjectOfType<GenerateCards>();
        idCard = 0;
    }
    private void Start()
    {
        HideCardInstantly();
    }
    void OnMouseDown()
    {
        if (!isShowing && generateCards.show)
        {
            ShowCard();
        }
    }
    //Change texture of card 
    public void SetTexture(Texture2D newtexture)
    {
        textureImage = newtexture;
    }
    //Show card image
    public void ShowCard()
    {
        if(!isShowing && generateCards.show)
        {
            isShowing = true;
            meshRenderer.material.mainTexture = textureImage;
            generateCards.SelectCard(this);
        } 
    }

    // Hide card image and show cover image
    public void HideCardInstantly()
    {
        meshRenderer.material.mainTexture = textureCover;
        isShowing = false;
        generateCards.show = true;
    }

    public void HideCard()
    {
        StartCoroutine(HideCardAfterDelay());
    }

    private IEnumerator HideCardAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        HideCardInstantly();
    }
}
