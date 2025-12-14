using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Lib;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];
    public Vector3 playerPosAjust = Vector3.zero;

    [Header("Acces publique pour autres scripts")]
    public Vector3 targetPos = Vector3.zero;

    GameObject[] listeCases;
    Vector3[] listeCasesPos;
    int rngMvt, movesLeft;
    bool allowInput = true, allowMove = false;
    List<Vector3> listeEndPos = new();

    //events
    public static event Action OnTurnEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Case.OnTileChoice += StartTileSelection;

        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));
        //listeCasesPos = new Vector3[listeCases.Length];
        //for (int i = 0; i < listeCases.Length; i++)
        //{
        //    //Debug.Log(listeCases[i].name);
        //    listeCasesPos[i] = listeCases[i].transform.position;
        //    //Debug.Log(listeCasesPos[i]);
        //}

        foreach(GameObject player in playerObjects)
        {
            player.transform.position = listeCases[0].transform.position + playerPosAjust;
        }
    }

    private void OnDestroy()
    {
        Case.OnTileChoice -= StartTileSelection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && allowInput)
        {
            //rngMvt = Random.Range(0, 7);
            //Debug.Log("rng mouvement: " + rngMvt);
            //int targetPos = gameSettings.Players[GameManager.playerTurn].CurrentPos;
            //for(int i = 1; i < (rngMvt + 1); i++)
            //{
            //    targetPos = ArrayMovement.CheckForResetLoop(targetPos + 1, listeCases.Length);
            //    listeEndPos.Add(listeCases[targetPos].transform.position);
            //    Debug.Log("derniere valeur de vecteur3: " + listeEndPos[^1] + "   nom gameobject: " + listeCases[targetPos].name);
            //    Debug.Log("nb end pos: " + listeEndPos.Count);
            //}

            movesLeft = Random.Range(0, 7);
            //Debug.Log("starting moves left: " + movesLeft);

            allowMove = true;
            allowInput = false;
        }

        if(allowMove)
        {
            //if (listeEndPos.Count > 0)
            //{
            //    //Debug.Log("can move");
            //    Vector3 endPos = listeEndPos[0] + playerPosAjust;
            //    //Debug.Log(endPos);
            //    float distance = Vector3.Distance(playerObjects[GameManager.playerTurn].transform.position, endPos);

            //    if (distance > 0)
            //    {
            //        playerObjects[GameManager.playerTurn].transform.position = Vector3.MoveTowards(playerObjects[GameManager.playerTurn].transform.position, endPos, .1f);
            //    }
            //    else
            //    {
            //        if (listeEndPos.Count == 1)
            //        {
            //            //Debug.Log("coord derniere pos: " + listeEndPos[0]);
            //            //Debug.Log("coord case finale: " + listeCases[gameSettings.Players[GameManager.playerTurn].CurrentPos + rngMvt].transform.position);
            //            //Debug.Log($"match? {listeEndPos[0] == listeCases[gameSettings.Players[GameManager.playerTurn].CurrentPos + rngMvt].transform.position}");
            //            //Debug.Log("index nouveau CurrentPos: " + Array.IndexOf(listeCasesPos, listeEndPos[0]));
            //            gameSettings.Players[GameManager.playerTurn].CurrentPos = Array.IndexOf(listeCasesPos, listeEndPos[0]);
            //        }
            //        listeEndPos.RemoveAt(0);
            //    }
            //}
            //else
            //{
            //    allowMove = false;
            //    allowInput = true;
            //}

            if(movesLeft > 0)
            {
                if(targetPos == Vector3.zero)
                {
                    targetPos = listeCases[gameSettings.Players[GameManager.playerTurn].CurrentPos + 1].transform.position + playerPosAjust;
                }
                float distance = Vector3.Distance(playerObjects[GameManager.playerTurn].transform.position, targetPos);

                if(distance > 0)
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
    }

    void StartTileSelection(Transform[] options)
    {
        allowMove = false;

        foreach(Transform option in options)
        {
            Debug.Log($"pos {option.name}: {option.position}");
        }
        Debug.Log($"somme des {options.Length} pos: {options[0].position + options[1].position}");
    }
}
