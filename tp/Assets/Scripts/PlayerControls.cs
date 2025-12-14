using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Lib;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerControls : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];
    public Vector3 playerPosAjust = Vector3.zero;
    public GameManager gameManager;

    [Header("Variables de test")]
    public int testCurrentPos = 0;

    [Header("Acces publique pour autres scripts")]
    public Vector3 targetPos = Vector3.zero;
    public bool allowTileChoice = false;

    GameObject[] listeCases;
    int movesLeft;
    bool allowInput = true, allowMove = false;

    //events
    public static event Action OnTurnEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Case.OnTileChoiceStart += StartTileSelection;

        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));

        for(int i = 0; i < playerObjects.Length; i++)
        {
            gameSettings.Players[i].CurrentPos = testCurrentPos;
            playerObjects[i].transform.position = listeCases[gameSettings.Players[i].CurrentPos].transform.position + playerPosAjust;
        }
    }

    private void OnDestroy()
    {
        Case.OnTileChoiceStart -= StartTileSelection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(gameSettings.Players[GameManager.playerTurn].Controls.Action) && allowInput)
        {
            GetMovesLeft();
        }

        if(allowMove)
        {
            MovePlayer();
        }

        if (allowTileChoice)
        {
            foreach(KeyCode key in gameSettings.Players[GameManager.playerTurn].Controls.AllControls)
            {
                if (Input.GetKeyDown(key))
                {
                    if(key == gameSettings.Players[GameManager.playerTurn].Controls.Action)
                    {
                        //Debug.Log(gameManager.tileChoice[0].gameObject.GetComponent<Case>().indexCase);
                        gameSettings.Players[GameManager.playerTurn].CurrentPos = gameManager.tileChoice[0].gameObject.GetComponent<Case>().indexCase - 1;
                        gameManager.popupTileChoice.SetActive(false);

                        //allowTileChoice = false;
                        //allowMove = true;
                    }
                    else
                    {
                        Array.Reverse(gameManager.tileChoice);
                        for(int i = 0; i < gameManager.tileChoiceiIndocators.Length; i++)
                        {
                            gameManager.tileChoiceiIndocators[i].transform.position = gameManager.tileChoice[i].transform.position + new Vector3(0, 10);
                        }
                    }
                }
            }
        }
    }



    void GetMovesLeft()
    {
        movesLeft = Random.Range(0, 7);
        //Debug.Log("starting moves left: " + movesLeft);

        allowMove = true;
        allowInput = false;
    }

    void MovePlayer()
    {
        if (movesLeft > 0)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = listeCases[gameSettings.Players[GameManager.playerTurn].CurrentPos + 1].transform.position + playerPosAjust;
            }
            float distance = Vector3.Distance(playerObjects[GameManager.playerTurn].transform.position, targetPos);

            if (distance > 0)
            {
                playerObjects[GameManager.playerTurn].transform.position = Vector3.MoveTowards(playerObjects[GameManager.playerTurn].transform.position, targetPos, .1f);
            }
            else
            {
                gameSettings.Players[GameManager.playerTurn].CurrentPos++;
                //Debug.Log($"new CurrentPos: {gameSettings.Players[GameManager.playerTurn].CurrentPos}    Vector3: {playerObjects[GameManager.playerTurn].transform.position}");s
                movesLeft--;
                //Debug.Log("current moves left: " + movesLeft);
                targetPos = Vector3.zero;
            }
        }
        else
        {
            //OnTurnEnd.Invoke();

            allowMove = false;
            allowInput = true;
        }
    }

    void StartTileSelection(Transform[] options)
    {
        foreach(Transform option in options)
        {
            Debug.Log($"pos {option.name}: {option.position}");
        }

        allowMove = false;
        allowTileChoice = true;
    }
}
