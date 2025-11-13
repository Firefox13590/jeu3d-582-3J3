using System;
using System.Collections.Generic;
using UnityEngine;
using Lib;
using static Lib.ArrayMovement;
using System.Linq;
using TMPro;
using Random = UnityEngine.Random;

public class TestCardHandler : MonoBehaviour
{
    public GameObject parentListeCarte;
    public GameObject selector; //the glow object
    public GameObject prefabCarte;
    public TestMvtSurCases playerScript;
    public TextMeshProUGUI affichageRolls;

    //RectTransform[][] rtrCartes = new RectTransform[2][];
    //RectTransform[,] rtrCartes = new RectTransform[5, 2];
    List<RectTransform> rtrCartes;
    float halvedLength;
    RectTransform rtrSelector;
    int selectorPos = 0;
    bool moveSelector = false;
    public bool allowInput = true;
    List<int> shuffledCardValues = new List<int>();

    /// <summary>
    /// Enum for the type of step used in <c>MoveTowards()</c>
    /// </summary>
    enum MoveTowardsSpeedType
    {
        Distance = 0,
        Time = 1
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halvedLength = (parentListeCarte.GetComponentsInChildren<RectTransform>().Length - 1) / 2f;
        //Debug.Log(halvedLength);
        //Debug.Log(Math.Ceiling(halvedLength));
        //Debug.Log(Math.Floor(halvedLength));

        rtrCartes = new List<RectTransform>(parentListeCarte.GetComponentsInChildren<RectTransform>()[1..]);
        //for(int i = 0; i < rtrCartes.Count; i++)
        //{
        //    //Debug.Log(rtrCartes[i].name);
        //    TestCardMvt cardMvtScript = rtrCartes[i].GetComponent<TestCardMvt>();
        //    //float xPosAdjustment, yPosAdjustment, gap, startPadding;

        //    //(xPosAdjustment, yPosAdjustment, gap, startPadding) = CalculTargetPosition(i);

        //    cardMvtScript.startPos = new Vector2(0, 400);
        //    cardMvtScript.targetPos = CalculTargetPosition(i);
        //    cardMvtScript.step = Vector2.Distance(cardMvtScript.startPos, cardMvtScript.targetPos) * Time.deltaTime / 2;
        //    cardMvtScript.enabled = true;
        //}
        PositionCards(rtrCartes);

        rtrSelector = selector.GetComponent<RectTransform>();
        rtrSelector.anchoredPosition = rtrCartes[selectorPos].anchoredPosition;
        selector.SetActive(true);

        //print(playerScript);
        playerScript.allowInput = false;
        ShuffleCards(shuffledCardValues);
    }

    // Update is called once per frame
    void Update()
    {
        if (allowInput)
        {
            // mouvement selecteur
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Going right");
                //moveSelector = true;
                //allowInput = false;
                //MoveUI(rtrSelector.anchoredPosition, rtrCartes[ArrayMovement.CheckForResetLoop(selectorPos + 1, rtrCartes.Count - 1)].anchoredPosition, MoveTowardsSpeedType.Time, 2);
                MoveSelector(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Going left");
                MoveSelector(1, reverse: true);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("Going up");
                MoveSelector((int)Math.Ceiling(halvedLength), reverse: true);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Going down");
                MoveSelector((int)Math.Ceiling(halvedLength));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                affichageRolls.text = "Roll: " + shuffledCardValues[selectorPos];
                Debug.Log("shuffledCardValue selected: " + shuffledCardValues[selectorPos]);
                playerScript.caseIncrease = Math.Abs(shuffledCardValues[selectorPos]);
                playerScript.reverseArrayCheck = (shuffledCardValues[selectorPos] < 0);
                if (shuffledCardValues[selectorPos] < 0)
                {
                    playerScript.comparaisonType = ComparaisonType.LessThanOrEqualTo;
                }
                else
                {
                    playerScript.comparaisonType = ComparaisonType.GreaterThanOrEqualTo;
                }
                //playerScript.allowInput = true;
                playerScript.AvancePlayer();
                allowInput = false;

                Destroy(rtrCartes[selectorPos].gameObject);
                rtrCartes.RemoveAt(selectorPos);
                shuffledCardValues.RemoveAt(selectorPos);
                parentListeCarte.SetActive(false);

                if (rtrCartes.Count == 0)
                {
                    selector.SetActive(false);
                    for (int i = 0; i < 10; i++)
                    {
                        GameObject instanceCarte = Instantiate(prefabCarte, parentListeCarte.GetComponent<RectTransform>());
                        instanceCarte.name += $"_{i}";
                    }
                    print(parentListeCarte.transform.childCount);
                    rtrCartes = new List<RectTransform>(parentListeCarte.GetComponentsInChildren<RectTransform>()[2..]);
                    print(rtrCartes.Count);
                    selector.SetActive(true);
                    ShuffleCards(shuffledCardValues);
                }
                //else
                //{
                halvedLength = (rtrCartes.Count) / 2f;
                PositionCards(rtrCartes);
                selectorPos = 0;
                rtrSelector.anchoredPosition = rtrCartes[selectorPos].anchoredPosition;
                //}
            }
        }

        //if (moveSelector)
        //{
        //    float baseSelectorDistance = Vector2.Distance(rtrCartes[selectorPos].anchoredPosition, rtrCartes[ArrayMovement.CheckForResetLoop(selectorPos + 1, rtrCartes.Count - 1)].anchoredPosition);
        //    //Debug.Log("baseSelectorDistance: " + baseSelectorDistance);

        //    rtrSelector.anchoredPosition = MoveUI
        //        (rtrSelector.anchoredPosition,
        //        rtrCartes[ArrayMovement.CheckForResetLoop(selectorPos + 1, rtrCartes.Count - 1)].anchoredPosition,
        //        ArrayMovement.CheckForResetLoop(selectorPos + 1, rtrCartes.Count - 1),
        //        MoveTowardsSpeedType.Time,
        //        moveTime: baseSelectorDistance * Time.deltaTime);
        //}
    }

    void CardListNavigation()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start">The start point</param>
    /// <param name="dest">The destination point</param>
    /// <param name="moveTowardsSpeedType">Type of movement. Either based on distance or time</param>
    /// <param name="moveTime">Time, in seconds for the movement. Only applies if moveTowardsSpeedType == MoveTowardsSpeedType.Time</param>
    /// <param name="moveDistance">step value for the movement. Only applies if moveTowardsSpeedType == MoveTowardsSpeedType.Distance</param>
    Vector2 MoveUI(Vector2 start, Vector2 dest, int targetIndex, MoveTowardsSpeedType moveTowardsSpeedType, float moveTime = 1, float moveDistance = .1f)
    {
        float distance = Vector2.Distance(start, dest);
        Vector2 updatedPos = start;

        if (distance > 0)
        {
            updatedPos = Vector2.MoveTowards(updatedPos, dest, moveTowardsSpeedType == MoveTowardsSpeedType.Time ? moveTime : moveDistance);
        }
        else
        {
            moveSelector = false;
            allowInput = true;
            selectorPos = targetIndex;
        }

        return updatedPos;
    }

    /// <summary>
    /// Handles the movement of the selector based on input
    /// </summary>
    /// <param name="moveIndicator">The number of indexes to move through.</param>
    /// <param name="comparaison">The comparison type to use for the movement.</param>
    /// <param name="reverse">Whether to reverse the movement direction or not (direction).</param>
    void MoveSelector(int moveIndicator, ComparaisonType comparaison = ComparaisonType.GreaterThan, bool reverse = false)
    {
        print(moveIndicator);
        int start = selectorPos;
        //print(reverse);
        if (reverse && (int)comparaison > 0)
        {
            comparaison = (ComparaisonType)(-(int)comparaison);
        }
        //print(comparaison);

        int end = CheckForLoopback(selectorPos, rtrCartes.Count - 1, moveIndicator, comparaison: comparaison, reverse: reverse);
        print($"start: {start}, end: {end}");
        rtrSelector.anchoredPosition = rtrCartes[end].anchoredPosition;
        selectorPos = end;
        //print(rtrSelector.anchoredPosition);
    }

    /// <summary>
    /// Processes the target position for a card based on its index in the list.
    /// </summary>
    /// <param name="index">Index of the card</param>
    /// <returns>The target position for the card</returns>
    Vector2 CalculTargetPosition(int index)
    {
        int xPosAdjustment, yPosAdjustment, indexAdjustment, gap = 300;
        xPosAdjustment = yPosAdjustment = indexAdjustment = 0;
        Vector2 targetPos;
        const int width = 150;

        if (index < Math.Ceiling(halvedLength))
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
                    xPosAdjustment = 75;
                    break;
                case 3:
                    xPosAdjustment = gap = 150;
                    break;
                case 2:
                    gap = 300;
                    xPosAdjustment = 225;
                    break;
                case 1:
                    xPosAdjustment = 450;
                    break;
            }
        }
        else
        {
            // bottom row
            indexAdjustment = (int)Math.Ceiling(halvedLength);

            switch (Math.Floor(halvedLength))
            {
                case 5:
                    gap = 75;
                    break;
                case 4:
                    gap = 100;
                    xPosAdjustment = 75;
                    break;
                case 3:
                    xPosAdjustment = gap = 150;
                    break;
                case 2:
                    gap = 300;
                    xPosAdjustment = 225;
                    break;
                case 1:
                    xPosAdjustment = 450;
                    break;
            }

        }

        targetPos = new Vector2((index - indexAdjustment) * width + ((index - indexAdjustment) * gap) + xPosAdjustment, yPosAdjustment);

        return targetPos;
    }

    /// <summary>
    /// Positions all cards in the list based on their index.
    /// </summary>
    /// <param name="rtrList">List of RectTransforms representing the cards' positioning.</param>
    void PositionCards(List<RectTransform> rtrList)
    {
        for (int i = 0; i < rtrList.Count; i++)
        {
            //Debug.Log(rtrList[i].name);
            //TestCardMvt cardMvtScript = rtrCartes[i].GetComponent<TestCardMvt>();
            //float xPosAdjustment, yPosAdjustment, gap, startPadding;

            //(xPosAdjustment, yPosAdjustment, gap, startPadding) = CalculTargetPosition(i);

            //cardMvtScript.startPos = new Vector2(0, 400);
            //cardMvtScript.targetPos = CalculTargetPosition(i);
            //cardMvtScript.step = Vector2.Distance(cardMvtScript.startPos, cardMvtScript.targetPos) * Time.deltaTime / 2;
            //cardMvtScript.enabled = true;

            rtrList[i].anchoredPosition = CalculTargetPosition(i);
        }
    }

    /// <summary>
    /// Randomizes the order of card values.
    /// </summary>
    /// <param name="values">List of card values to shuffle.</param>
    void ShuffleCards(List<int> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            values.Add(Random.Range(-7, 7));
        }
        values.OrderBy(x => Random.value).ToList();
    }
}
