using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Texture2D textureCover;
    public Texture2D textureImage;
    public Vector3 positionOriginal;
    public GameObject createCard;

    public int idCard = 0;
    public float timeDelay;
    public bool showing;



    private void Awake()
    {
        createCard = GameObject.Find("CardsRandom");
    }
    private void Start()
    {
        HideCard();
    }
    void OnMouseDown()
    {
        Debug.Log(idCard.ToString());
        ShowCard();
    }
    //Change texture of card 
    public void ChangeTexture(Texture2D _texture)
    {
        textureImage = _texture;
    }
    //Show card image
    public void ShowCard()
    {
        if(!showing && createCard.GetComponent<GenerateCards>().show)
        {
            showing = true;
            GetComponent<MeshRenderer>().material.mainTexture = textureImage;
            createCard.GetComponent<GenerateCards>().SelectCard(this);
        } 
    }

    // Hide card image and show cover image
    public void HideCard()
    {
        Invoke("Hide", timeDelay);
        createCard.GetComponent<GenerateCards>().show = false;  
    }

    public void Hide()
    {
        GetComponent<MeshRenderer>().material.mainTexture = textureCover;
        showing = false;
        createCard.GetComponent<GenerateCards>().show = true;
    }
}
