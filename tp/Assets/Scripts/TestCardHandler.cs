using System;
using UnityEngine;

public class TestCardHandler : MonoBehaviour
{
    public GameObject parentListeCarte;
    RectTransform[][] rtrCartes = new RectTransform[2][];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float halvedLength = (parentListeCarte.GetComponentsInChildren<RectTransform>().Length - 1) / 2;
        Debug.Log(halvedLength);
        //Debug.Log(Math.Ceiling(halvedLength));
        //Debug.Log(Math.Floor(halvedLength));
        rtrCartes[0] = parentListeCarte.GetComponentsInChildren<RectTransform>()[1..((int) Math.Ceiling(halvedLength) + 1)];
        rtrCartes[1] = parentListeCarte.GetComponentsInChildren<RectTransform>()[((int) Math.Floor(halvedLength) + 1)..];
        Debug.Log("premiere partie: " + rtrCartes[0].Length + "\ndeuxieme partie: " + rtrCartes[1].Length);

        for(int i = 0; i < rtrCartes.Length; i++)
        {
            for(int j = 0; j < rtrCartes[i].Length; j++)
            {
                //Debug.Log("rtrCartes[" + i + "][" + j + "] = " + rtrCartes[i][j].name);
                rtrCartes[i][j].anchoredPosition = new Vector2(-500, 400);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
