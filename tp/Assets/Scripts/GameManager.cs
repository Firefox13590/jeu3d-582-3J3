using System;
using UnityEngine;
using TMPro;
using Lib;

public class GameManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public TextMeshProUGUI popupTileChoice;
    public GameSettingsScriptableObject gameSettings;
    public PlayerControls playerControls;
    public GameObject[] tileChoiceiIndocators = new GameObject[2];

    // variables
    public static int playerTurn = 0;

    private void Awake()
    {
        gameSettings.SetBackDefaultPlayerSettings();
    }

    private void Start()
    {
        Case.OnTileChoice += DisplayTileChoiceText;
        PlayerControls.OnTurnEnd += UpdatePlayerTurn;
    }

    private void OnDestroy()
    {
        Case.OnTileChoice -= DisplayTileChoiceText;
        PlayerControls.OnTurnEnd -= UpdatePlayerTurn;
    }



    void DisplayTileChoiceText(Transform[] options)
    {
        popupTileChoice.gameObject.SetActive(true);
        for(int i = 0; i < options.Length; i++)
        {
            tileChoiceiIndocators[i].transform.position = options[i].transform.position + new Vector3(0, 10, 0);
            tileChoiceiIndocators[i].SetActive(true);
        }
    }

    void UpdatePlayerTurn()
    {
        playerTurn = ArrayMovement.CheckForResetLoop(playerTurn + 1, 4);
    }
}
