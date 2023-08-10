using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    public Text playerName;
    public GameObject panelWin;

    public bool showName;

    private void Start()
    {
        // Mostrar el nombre del jugador en el objeto de texto
        playerName.text = GameManager.Instance.playerName;
    }
}
