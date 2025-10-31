using System;
using System.Collections.Generic;
using UnityEngine;

public class TestCardHandler : MonoBehaviour
{
    public GameObject parentListeCarte;
    public GameObject selector; //the glow object

    //RectTransform[][] rtrCartes = new RectTransform[2][];
    //RectTransform[,] rtrCartes = new RectTransform[5, 2];
    List<RectTransform> rtrCartes;
    float halvedLength;
    RectTransform rtrSelector;
    int selectorPos = 0;
    bool moveSelector = false, allowInput = true;

    enum MoveTowardsSpeedType
    {
        Distance = 0,
        Time = 1
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halvedLength = (parentListeCarte.GetComponentsInChildren<RectTransform>().Length - 1) / 2;
        //Debug.Log(halvedLength);
        //Debug.Log(Math.Ceiling(halvedLength));
        //Debug.Log(Math.Floor(halvedLength));
        //rtrCartes[0] = parentListeCarte.GetComponentsInChildren<RectTransform>()[1..((int) Math.Ceiling(halvedLength) + 1)];
        //rtrCartes[1] = parentListeCarte.GetComponentsInChildren<RectTransform>()[((int) Math.Floor(halvedLength) + 1)..];
        //Debug.Log("premiere partie: " + rtrCartes[0].Length + "\ndeuxieme partie: " + rtrCartes[1].Length);

        //for(int i = 0; i < rtrCartes.Length; i++)
        //{
        //    for(int j = 0; j < rtrCartes[i].Length; j++)
        //    {
        //        //Debug.Log("rtrCartes[" + i + "][" + j + "] = " + rtrCartes[i][j].name);
        //        rtrCartes[i][j].anchoredPosition = new Vector2(-500, 400);
        //    }
        //}


        rtrCartes = new List<RectTransform>(parentListeCarte.GetComponentsInChildren<RectTransform>()[1..]);
        for(int i = 0; i < rtrCartes.Count; i++)
        {
            //Debug.Log(rtrCartes[i].name);
            TestCardMvt cardMvtScript = rtrCartes[i].GetComponent<TestCardMvt>();
            //float xPosAdjustment, yPosAdjustment, gap, startPadding;

            //(xPosAdjustment, yPosAdjustment, gap, startPadding) = CalculTargetPosition(i);

            cardMvtScript.startPos = new Vector2(0, 400);
            //cardMvtScript.targetPos = new Vector2(-300 + (i * 150), 0);
            cardMvtScript.targetPos = CalculTargetPosition(i);
            cardMvtScript.step = Vector2.Distance(cardMvtScript.startPos, cardMvtScript.targetPos) * Time.deltaTime / 2;
            cardMvtScript.enabled = true;
        }

        rtrSelector = selector.GetComponent<RectTransform>();
        rtrSelector.anchoredPosition = rtrCartes[selectorPos].anchoredPosition;
        selector.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (allowInput)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Going right");
                moveSelector = true;
                allowInput = false;
                MoveUI(rtrSelector, rtrCartes[selectorPos + 1].anchoredPosition, MoveTowardsSpeedType.Time, 2);
            }
        }

        if (moveSelector)
        {
            
        }
    }

    void CardListNavigation()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item">The UI element to move around</param>
    /// <param name="dest">The destination point</param>
    /// <param name="moveTowardsSpeedType">Type of movement. Either based on distance or time</param>
    /// <param name="moveTime">Time, in seconds for the movement. Only applies if moveTowardsSpeedType == MoveTowardsSpeedType.Time</param>
    /// <param name="moveDistance">step value for the movement. Only applies if moveTowardsSpeedType == MoveTowardsSpeedType.Distance</param>
    void MoveUI(RectTransform item, Vector2 dest, MoveTowardsSpeedType moveTowardsSpeedType, float moveTime = 1, float moveDistance = .1f)
    {
        allowInput = false;
        Vector2 start = item.anchoredPosition;
        float distance = Vector2.Distance(item.anchoredPosition, dest);

        while(distance > 0)
        {
            item.anchoredPosition = Vector2.MoveTowards(item.anchoredPosition, dest, moveTowardsSpeedType == MoveTowardsSpeedType.Time ? 
                (Vector2.Distance(start, dest) * (moveTime * Time.deltaTime)) : moveDistance);
            distance = Vector2.Distance(item.anchoredPosition, dest);
        }

        allowInput = true;
    }

    Vector2 CalculTargetPosition(int index)
    {
        int yPosAdjustment = 0, gap = 300, indexAdjustment = 0;
        Vector2 targetPos;
        const int width = 150;

        if (index < halvedLength)
        {
            // top row
            yPosAdjustment = 400;

            switch (Math.Ceiling(halvedLength))
            {
                case 5:
                    gap = 75;
                    break;
                case 4:
                    gap = 100;
                    break;
                case 3:
                    gap = 150;
                    break;
            }
        }
        else
        {
            // bottom row
            indexAdjustment = (int) Math.Ceiling(halvedLength);

            switch (Math.Floor(halvedLength))
            {
                case 5:
                    gap = 75;
                    break;
                case 4:
                    gap = 100;
                    break;
                case 3:
                    gap = 150;
                    break;
            }

        }

        targetPos = new Vector2((index - indexAdjustment) * width + ((index - indexAdjustment) * gap), yPosAdjustment);

        return targetPos;
    }
}
