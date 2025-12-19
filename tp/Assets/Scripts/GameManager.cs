using System;
using UnityEngine;
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
    public static int turnsLeft = 5;

    private void Awake()
    {
        gameSettings.SetBackDefaultPlayerSettings();
    }

    private void Start()
    {
        // abonnement aux évènements
        Case.OnTileChoiceStart += DisplayTileChoice;
        PlayerControls.OnTurnEnd += UpdatePlayerTurn;
        PlayerControls.OnTileChoiceEnd += HideTileChoice;
    }

    private void OnDestroy()
    {
        // désabonnement aux évènements
        Case.OnTileChoiceStart -= DisplayTileChoice;
        PlayerControls.OnTurnEnd -= UpdatePlayerTurn;
        PlayerControls.OnTileChoiceEnd -= HideTileChoice;
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
    /// Cache le popup de choix de case ainsi que les indicateurs de sélection.
    /// </summary>
    void HideTileChoice()
    {
        popupTileChoice.SetActive(false);
        foreach (GameObject obj in tileChoiceiIndocators)
        {
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// Change la position des indicateurs de sélection.
    /// </summary>
    public void ChangeTileSelection()
    {
        Array.Reverse(tileChoice);
        for (int i = 0; i < tileChoiceiIndocators.Length; i++)
        {
            tileChoiceiIndocators[i].transform.position = tileChoice[i].transform.position + new Vector3(0, 10);
        }
    }

    /// <summary>
    /// Change l'ordre du tour des joueurs.
    /// </summary>
    void UpdatePlayerTurn()
    {
        playerTurn = ArrayMovement.CheckForResetLoop(playerTurn + 1, gameSettings.Players.Length);
        //Debug.Log($"player type: {gameSettings.Players[playerTurn].GetType()}    player name: {gameSettings.Players[playerTurn].Name}");

        if(playerTurn == 0)
        {
            turnsLeft--;
        }
    }
}
