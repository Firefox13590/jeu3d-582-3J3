using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    public bool changementChemin = false;
    public Transform[] optionsCase;

    public int indexCase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexCase = Int32.Parse(name[5..]);
    }

    void GiveTileChoice()
    {
        foreach(Transform option in optionsCase)
        {
            Debug.Log(option.name);
        }
    }
}
