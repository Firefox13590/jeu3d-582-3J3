using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public GameSettingsScriptableObject gameSettings;
    public GameObject[] playerObjects = new GameObject[4];

    GameObject[] listeCases;
    int rngMvt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listeCases = GameObject.FindGameObjectsWithTag("Case");
        Array.Sort(listeCases, (a, b) => string.CompareOrdinal(a.name, b.name));
        foreach (GameObject obj in listeCases)
        {
            Debug.Log(obj.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rngMvt = Random.Range(0, 7);
        }
    }
}
