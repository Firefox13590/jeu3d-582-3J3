using System;
using UnityEngine;

public class Case : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public bool choixChemin = false;
    public Transform[] optionsCase;
    public Transform forceProchaineCase;

    [Header("Acces publique pour autres scripts")]
    public int indexCase;

    public static event Action<Transform[]> OnTileChoiceStart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexCase = Int32.Parse(name[5..]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (choixChemin && other.gameObject.CompareTag("Player"))
        {
            OnTileChoiceStart.Invoke(optionsCase);
        }
    }
}
