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

    // variables statiques
    public static int playerTurn = 0;

    private void Awake()
    {
        gameSettings.SetBackDefaultPlayerSettings();
    }

    private void Start()
    {
        Case.OnTileChoiceStart += DisplayTileChoiceText;
        PlayerControls.OnTurnEnd += UpdatePlayerTurn;
    }

    private void OnDestroy()
    {
        Case.OnTileChoiceStart -= DisplayTileChoiceText;
        PlayerControls.OnTurnEnd -= UpdatePlayerTurn;
    }



    void DisplayTileChoiceText(Transform[] options)
    {
        tileChoice = options;
        popupTileChoice.SetActive(true);

        for(int i = 0; i < options.Length; i++)
        {
            tileChoiceiIndocators[i].transform.position = options[i].transform.position + new Vector3(0, 10);
            tileChoiceiIndocators[i].SetActive(true);
        }
    }

    void UpdatePlayerTurn()
    {
        playerTurn = ArrayMovement.CheckForResetLoop(playerTurn + 1, 4);
    }
}
