using UnityEngine;
using TMPro;

public class TexteClignotant : MonoBehaviour
{
    bool estAffiche = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(Clignote), 0, .75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clignote()
    {
        if (estAffiche)
        {
            estAffiche = false;
            gameObject.SetActive(false);
        }
        else
        {
            estAffiche = true;
            gameObject.SetActive(true);
        }
    }
}
