using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    public bool offreChoix = false;
    public Transform[] optionsCase;

    public int indexCase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexCase = Int32.Parse(name[5..]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
