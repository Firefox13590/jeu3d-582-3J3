using System.Linq;
using UnityEngine;

public class TestCardSpinning : MonoBehaviour
{
    public GameObject cardList;
    [Range(0.1f, 10)]
    public float vitesseRotationBase = 1;

    RectTransform[] rtrCardList;
    float[] vitessesRotationRandom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // recuperer liste cartes (avec parent empty)
        rtrCardList = cardList.GetComponentsInChildren<RectTransform>();
        //List<RectTransform> temp = rtransCards.ToList();
        // enlever premier element (parent empty)
        rtrCardList = rtrCardList.Skip(1).ToArray();
        Debug.Log("nb cartes: " + rtrCardList.Length);

        // liste vitesses random rotation pour cartes
        vitessesRotationRandom = new float[rtrCardList.Length];
        for(int i = 0; i < rtrCardList.Length; i++)
        {
            Debug.Log(rtrCardList[i]);
            vitessesRotationRandom[i] = Random.Range(-vitesseRotationBase, vitesseRotationBase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < rtrCardList.Length; i++)
        {
            rtrCardList[i].Rotate(0, vitessesRotationRandom[i], 0);
        }
    }
}
