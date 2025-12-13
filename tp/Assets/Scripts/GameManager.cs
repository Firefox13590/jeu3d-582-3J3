using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //valeurs a ajuster dans l'inspecteur
    public TextMeshProUGUI popupTileChoice;
    public GameSettingsScriptableObject gameSettings;

    // variables
    public static int playerTurn = 0;

    private void Awake()
    {
        gameSettings.SetBackDefaultPlayerSettings();
    }

    private void Start()
    {
        Case.OnTileChoice += DisplayTileChoiceText;
    }



    void DisplayTileChoiceText(Transform[] _)
    {
        popupTileChoice.gameObject.SetActive(true);
    }
}
