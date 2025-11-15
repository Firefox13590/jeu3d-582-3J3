using TMPro;
using UnityEngine;

public class TexteCompteur : MonoBehaviour
{
    public MouvementsHelicoptere scriptHelico;
    public int compteur = 150;
    TextMeshProUGUI texteCompteur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texteCompteur = GetComponent<TextMeshProUGUI>();
        texteCompteur.text = compteur.ToString();

        // Appelle Compteur() chaque seconde apres 1 seconde
        InvokeRepeating(nameof(Compteur), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Diminue valeur compteur et met à jour le texte.
    /// <para>
    /// Si compteur atteint 0, arrête InvokeRepeating et fait exploser l'helico via kaboomHelicoptere().
    /// </para>
    /// </summary>
    void Compteur()
    {
        //Debug.Log(compteur);
        compteur--;
        texteCompteur.text = compteur.ToString();
        //Debug.Log(texteCompteur.text);

        if (compteur <= 0)
        {
            CancelInvoke();
            scriptHelico.KaboomHelicoptere();
        }
    }
}
