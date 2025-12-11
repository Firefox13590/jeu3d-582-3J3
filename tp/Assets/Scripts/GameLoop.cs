using System;
using UnityEngine;
using TMPro;

public class GameLoop : MonoBehaviour
{
    //valeurs a ajuster dans l'inspecteur
    public TextMeshProUGUI popupTileChoice;

    // variables
    public static int playerTurn = 0;

    // events
    public static event Action OnTileChoice;
}
