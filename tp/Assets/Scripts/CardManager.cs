using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameObject glow, cartePrefab;
    public Material[] matCartes = new Material[8];

    [Header("Acces publique pour autres scripts")]
    public List<GameObject> listeCartes;

    // variables privées
    float halfPoint;
    int glowPos = 0;
    // Use this for initialization
    void Start()
    {
        RemplirListeCartes();

        // obtention du point milieu
        halfPoint = listeCartes.Count / 2f;
        //Debug.Log(halfPoint);
    }



    void RemplirListeCartes()
    {
        for(int i = 0; i < 10; i++)
        {
            // instanciation
            GameObject instanceCarte = Instantiate(cartePrefab);

            // paramétrage de l'instance
            instanceCarte.name = "Carte_" + i;
            instanceCarte.GetComponent<Image>().material = matCartes[Random.Range(0, matCartes.Length)];
            //Debug.Log("valeur carte: " + instanceCarte.GetComponent<Image>().material.name[4]);
            instanceCarte.transform.SetParent(gameObject.transform, false);

            // ajout de l'instance à la liste
            listeCartes.Add(instanceCarte);
        }

        //halfPoint = listeCartes.Count / 2f;
        PositionnerCartes();
    }

    void PositionnerCartes()
    {
        halfPoint = listeCartes.Count / 2f;

        int ajustedIndex, nbCartesRangee, row, ajustedX, gap = 100;
        const int width = 200;
        glowPos = ajustedX = 0;

        for (int i = 0; i < listeCartes.Count; i++)
        {
            ajustedIndex = i;
            nbCartesRangee = (int)Math.Ceiling(halfPoint);
            //Debug.Log("float halfPoint: " + halfPoint);
            //Debug.Log("int halfPoint: " + (int)Math.Ceiling(halfPoint));

            if (i < Math.Ceiling(halfPoint))
            {
                // rangée du haut
                row = 0;
                //Debug.Log("rangee haut: " + i);
            }
            else
            {
                // rangée du bas
                row = 1;
                ajustedIndex -= (int)Math.Ceiling(halfPoint);
                //Debug.Log($"rangee bas: {i}    ajustedIndex: {ajustedIndex}");
                nbCartesRangee = (int)Math.Floor(halfPoint);
            }
            switch (nbCartesRangee)
            {
                case 5:
                    gap = 100;
                    ajustedX = 0;
                    break;
                case 4:
                    gap = 150;
                    ajustedX = 75;
                    break;
                case 3:
                    gap = 300;
                    ajustedX = 100;
                    break;
                case 2:
                    gap = 400;
                    ajustedX = 300;
                    break;
                case 1:
                    gap = 0;
                    ajustedX = 600;
                    break;
            }

            listeCartes[i].GetComponent<RectTransform>().anchoredPosition = new Vector2
                ((-600 + ajustedX) + ajustedIndex * (width + gap)/* - 600*/,
                200 - 400 * row);
            Debug.Log($"new vector2 for {listeCartes[i].name}: {listeCartes[i].GetComponent<RectTransform>().anchoredPosition}");
        }

        glow.GetComponent<RectTransform>().anchoredPosition = listeCartes[glowPos].GetComponent<RectTransform>().anchoredPosition;
    }

    public void ChoisirCarte()
    {
        Destroy(listeCartes[glowPos]);
        listeCartes.RemoveAt(glowPos);
        //Debug.Log(listeCartes.Count);

        if(listeCartes.Count == 0)
        {
            RemplirListeCartes();
        }
        else
        {
            PositionnerCartes();
        }
    }
}
