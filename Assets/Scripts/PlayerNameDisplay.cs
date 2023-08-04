using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    public Text playerName;

    private void Start()
    {
        // Mostrar el nombre del jugador en el objeto de texto
        playerName.text = GameManager.Instance.playerName;
        Debug.Log("Pglo" + playerName.text);
    }
}
