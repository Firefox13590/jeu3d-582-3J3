using System.Linq;
using UnityEngine;

public class TestCardSpinning : MonoBehaviour
{
    public GameObject cardList;
    [Range(0.1f, 10)]
    public float vitesseRotationBase = 1;

    RectTransform[] rtransCards;
    float[] vitessesRotationRandom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rtransCards = cardList.GetComponentsInChildren<RectTransform>();
        //List<RectTransform> temp = rtransCards.ToList();
        rtransCards = rtransCards.Skip(1).ToArray();
        Debug.Log("nb cartes: " + rtransCards.Length);

        vitessesRotationRandom = new float[rtransCards.Length];
        for(int i = 0; i < rtransCards.Length; i++)
        {
            Debug.Log(rtransCards[i]);
            vitessesRotationRandom[i] = Random.Range(-vitesseRotationBase, vitesseRotationBase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < rtransCards.Length; i++)
        {
            rtransCards[i].Rotate(0, vitessesRotationRandom[i], 0);
        }
    }
}
