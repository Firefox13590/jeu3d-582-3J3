using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Lib;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    //objets unity a lier dans l'inspecteur
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];


    GameObject[] listeCases;
    int rngMvt, currentPos;
    bool allowInput = true, allowMove = false;
    List<Vector3> listEndPos = new List<Vector3>();
    Vector3 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));
        //foreach (GameObject obj in listeCases)
        //{
        //    Debug.Log(obj.name);
        //}

        playerPos = playerObjects[GameLoop.playerTurn].transform.position;
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
                listEndPos.Add(listeCases[targetPos].transform.position);
                Debug.Log("derniere valeur de vecteur3: " + listEndPos[^1] + "   nom gameobject: " + listeCases[targetPos].name);
                Debug.Log("nb end pos: " + listEndPos.Count);
            }
            allowMove = true;
            allowInput = false;
        }

        if(allowMove)
        {
            if(listEndPos.Count > 0)
            {
                //Debug.Log("can move");
                Vector3 endPos = listEndPos[0];
                //Debug.Log(endPos);
                float distance = Vector3.Distance(playerPos, endPos);

                if (distance > 0)
                {
                    playerPos = Vector3.MoveTowards(playerPos, endPos, .1f);
                }
                else
                {
                    listEndPos.RemoveAt(0);
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
