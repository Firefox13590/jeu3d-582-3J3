using System;
using UnityEngine;
using UnityEngine.UI;
using Lib;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameObject popupTileChoice;
    public GameSettingsScriptableObject gameSettings;
    public PlayerControls playerControls;
    public GameObject[] tileChoiceiIndocators = new GameObject[2], playerStatsPanels = new GameObject[4];
    public Material[] playerMaterials = new Material[4];

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

        //Debug.Log(playerMaterials[0].color);
        HighlightPlayerStatsPanel();
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
    /// Met à jour l'ordre du tour des joueurs.
    /// </summary>
    /// <remarks>À chaque fois que <see cref="playerTurn"/> revient à 0, décrémente <see cref="turnsLeft"/></remarks>
    void UpdatePlayerTurn()
    {
        AddCurrency();

        playerTurn = ArrayMovement.CheckForResetLoop(playerTurn + 1, gameSettings.Players.Length);
        //Debug.Log($"player type: {gameSettings.Players[playerTurn].GetType()}    player name: {gameSettings.Players[playerTurn].Name}");
        HighlightPlayerStatsPanel();

        if(playerTurn == 0)
        {
            turnsLeft--;
        }
    }

    void HighlightPlayerStatsPanel()
    {
        for(int i = 0; i < playerStatsPanels.Length; i++)
        {
            if(i == playerTurn)
            {
                playerStatsPanels[i].GetComponent<RectTransform>().sizeDelta = new Vector2(450, 350);
                playerStatsPanels[i].GetComponentInChildren<Image>().color = playerMaterials[i].color;
            }
            else
            {
                playerStatsPanels[i].GetComponent<RectTransform>().sizeDelta = new Vector2(450, 300);
                playerStatsPanels[i].GetComponentInChildren<Image>().color = new Color32(0, 0, 0, 100);
            }
        }
    }

    void AddCurrency()
    {
        //Debug.Log(playerStatsPanels[playerTurn].transform.childCount);
        TextMeshProUGUI textCurrency = playerStatsPanels[playerTurn].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        //Debug.Log(textCurrency.text);
        textCurrency.text = "x  " + (int.Parse(textCurrency.text[3..]) + Random.Range(0, 8));
    }
}
