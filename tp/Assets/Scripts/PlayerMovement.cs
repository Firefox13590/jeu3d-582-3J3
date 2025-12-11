using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Lib;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    //valeurs a ajuster dans l'inspecteur
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];
    public Vector3 playerPosAjust = Vector3.zero;


    GameObject[] listeCases;
    Vector3[] listeCasesPos;
    int rngMvt, currentPos;
    bool allowInput = true, allowMove = false;
    List<Vector3> listeEndPos = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));
        listeCasesPos = new Vector3[listeCases.Length];
        for (int i = 0; i < listeCases.Length; i++)
        {
            //Debug.Log(listeCases[i].name);
            listeCasesPos[i] = listeCases[i].transform.position;
            //Debug.Log(listeCasesPos[i]);
        }

        //playerPos = playerObjects[GameLoop.playerTurn].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && allowInput)
        {
            rngMvt = Random.Range(0, 7);
            Debug.Log("rng mouvement: " + rngMvt);
            int targetPos = gameSettings.Players[GameLoop.playerTurn].CurrentPos;
            for(int i = 1; i < (rngMvt + 1); i++)
            {
                targetPos = ArrayMovement.CheckForResetLoop(targetPos + 1, listeCases.Length);
                listeEndPos.Add(listeCases[targetPos].transform.position);
                Debug.Log("derniere valeur de vecteur3: " + listeEndPos[^1] + "   nom gameobject: " + listeCases[targetPos].name);
                Debug.Log("nb end pos: " + listeEndPos.Count);
            }
            allowMove = true;
            allowInput = false;
        }

        if(allowMove)
        {
            if (listeEndPos.Count > 0)
            {
                //Debug.Log("can move");
                Vector3 endPos = listeEndPos[0] + playerPosAjust;
                //Debug.Log(endPos);
                float distance = Vector3.Distance(playerObjects[GameLoop.playerTurn].transform.position, endPos);

                if (distance > 0)
                {
                    playerObjects[GameLoop.playerTurn].transform.position = Vector3.MoveTowards(playerObjects[GameLoop.playerTurn].transform.position, endPos, .1f);
                }
                else
                {
                    if (listeEndPos.Count == 1)
                    {
                        //Debug.Log("coord derniere pos: " + listeEndPos[0]);
                        //Debug.Log("coord case finale: " + listeCases[gameSettings.Players[GameLoop.playerTurn].CurrentPos + rngMvt].transform.position);
                        //Debug.Log($"match? {listeEndPos[0] == listeCases[gameSettings.Players[GameLoop.playerTurn].CurrentPos + rngMvt].transform.position}");
                        //Debug.Log("index nouveau CurrentPos: " + Array.IndexOf(listeCasesPos, listeEndPos[0]));
                        gameSettings.Players[GameLoop.playerTurn].CurrentPos = Array.IndexOf(listeCasesPos, listeEndPos[0]);
                    }
                    listeEndPos.RemoveAt(0);
                }
            }
            else
            {
                allowMove = false;
                allowInput = true;
            }
        }
    }
}
