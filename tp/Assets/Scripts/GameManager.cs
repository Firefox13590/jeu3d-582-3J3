using System;
using UnityEngine;
using TMPro;
using Lib;

public class GameManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameObject popupTileChoice;
    public GameSettingsScriptableObject gameSettings;
    public PlayerControls playerControls;
    public GameObject[] tileChoiceiIndocators = new GameObject[2];

    [Header("Acces publique pour autres scripts")]
    public Transform[] tileChoice;

    // variables publiques statiques
    public static int playerTurn = 0;

    private void Awake()
    {
        gameSettings.SetBackDefaultPlayerSettings();
    }

    private void Start()
    {
        // abonnement aux évènements
        Case.OnTileChoiceStart += DisplayTileChoice;
        PlayerControls.OnTurnEnd += UpdatePlayerTurn;
    }

    private void OnDestroy()
    {
        // désabonnement aux évènements
        Case.OnTileChoiceStart -= DisplayTileChoice;
        PlayerControls.OnTurnEnd -= UpdatePlayerTurn;
    }



    /// <summary>
    /// Fait apparaitre le popup de choix de case.
    /// </summary>
    /// <param name="options">Les options de case</param>
    void DisplayTileChoice(Transform[] options)
    {
        tileChoice = options;
        popupTileChoice.SetActive(true);

        for(int i = 0; i < options.Length; i++)
        {
            tileChoiceiIndocators[i].transform.position = options[i].transform.position + new Vector3(0, 10);
            tileChoiceiIndocators[i].SetActive(true);
        }
    }

    /// <summary>
    /// Change l'ordre du tour des joueurs.
    /// </summary>
    void UpdatePlayerTurn()
    {
        playerTurn = ArrayMovement.CheckForResetLoop(playerTurn + 1, 4);
        Debug.Log(gameSettings.Players[playerTurn].GetType());
    }
}
