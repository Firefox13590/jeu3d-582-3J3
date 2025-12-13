using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    public bool choixChemin = false;
    public Transform[] optionsCase;
    public Transform forceProchaineCase;

    public int indexCase;

    public static event Action<Transform[]> OnTileChoice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexCase = Int32.Parse(name[5..]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (choixChemin && other.gameObject.CompareTag("Player"))
        {
            OnTileChoice.Invoke(optionsCase);
        }
    }
}
