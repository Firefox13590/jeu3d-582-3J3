using System.Linq;
using UnityEngine;
using Lib;
using static Lib.ArrayMovement.ComparaisonType;

public class TestMvtSurCases : MonoBehaviour
{
    public GameObject caseList;
    public int caseIncrease = 3;
    public float tempsMvtJoueur = 1;

    Transform[] trCaseList;
    int currentPos = 0;
    TestPlayerMoveTowards frontendScript;
    public bool allowInput = true;
    public bool reverseArrayCheck = false;
    public ArrayMovement.ComparaisonType comparaisonType = GreaterThan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // recuperer liste case (avec parent empty)
        trCaseList = caseList.GetComponentsInChildren<Transform>();
        // enlever premier element (parent empty) avec spread operator
        // change valeurs du array pour commencer a l'index 1 (seulement les cases)
        trCaseList = trCaseList[1..];
        //Debug.Log("nb cases: " + trCaseList.Length);
        transform.position = trCaseList[currentPos].position;
        frontendScript = GetComponent<TestPlayerMoveTowards>();
        //print("caseIncrease: " + caseIncrease + "    reverse: " + reverseArrayCheck);
    }

    /// <summary>
    /// Calculates player movement across tiles.
    /// </summary>
    /// <param name="currentPos">Current position of the player.</param>
    /// <param name="movesLeft">Number of moves left to make.</param>
    /// <param name="trCaseList">List of all tiles.</param>
    int CalculAvanceCase(int currentPos, int movesLeft, Transform[] trCaseList)
    {
        Debug.Log($"debut pre-check loop: {currentPos}\nmoves left: {movesLeft}");
        int nextPos, maxPos = trCaseList.Length - 1;
        frontendScript.startPos = trCaseList[ArrayMovement.CheckForResetLoop(currentPos, maxPos, comparaison: comparaisonType, reverse: reverseArrayCheck)].position;
        Debug.Log("debut post-check loop: " + currentPos);

        while (movesLeft > 0)
        {
            currentPos = ArrayMovement.CheckForResetLoop(currentPos, maxPos, comparaison: comparaisonType, reverse: reverseArrayCheck);
            if (!reverseArrayCheck)
            {
                nextPos = currentPos + 1;
            }
            else
            {
                nextPos = currentPos - 1;
            }
            nextPos = ArrayMovement.CheckForResetLoop(nextPos, maxPos, comparaison: comparaisonType, reverse: reverseArrayCheck);
            Debug.Log($"pos 1 index:{currentPos}\npos 2 index:{nextPos}");
            MovePlayer(trCaseList[currentPos].position, trCaseList[nextPos].position, tempsMvtJoueur);
            currentPos = nextPos;
            movesLeft--;
        }

        frontendScript.enabled = true;

        return currentPos;
    }

    /// <summary>
    /// Moves the player from the start position to the end position over a specified duration.
    /// </summary>
    /// <param name="startPos">The starting position of the player.</param>
    /// <param name="endPos">The target position of the player.</param>
    /// <param name="elapsedTime">The time taken to move from start to end, in seconds. Defaults to 1.</param>
    /// <param name="frames">The number of frames over which to interpolate the movement. Defaults to 30</param>
    void MovePlayer(Vector3 startPos, Vector3 endPos, float elapsedTime = 1, int frames = 30)
    {
        //Debug.Log(startPos.magnitude);
        //Debug.Log(startPos.sqrMagnitude);
        //transform.position = endPos;

        float distance = Vector3.Distance(startPos, endPos);
        //Debug.Log("distance: " + distance);
        //float cooldown = elapsedTime / frames;
        //float oneFrameDistance = distance * cooldown;
        float oneFrameDistance = distance / (elapsedTime * frames);
        //Debug.Log("oneframeDistance: " + oneFrameDistance);
        //Debug.Log("cooldown: " + cooldown);
        //Debug.Log("deltaTime: " + Time.deltaTime);
        //while(distance > 0)
        //{
        //    //cooldown -= Time.deltaTime;
        //    while (cooldown > 0)
        //    {
        //        cooldown -= Time.deltaTime;
        //        Debug.Log(cooldown);
        //    }
        //    //transform.position = startPos = Vector3.MoveTowards(player.transform.position, endPos, vitesseMvtJoueur * Time.deltaTime);
        //    transform.position = startPos = Vector3.MoveTowards(startPos, endPos, oneFrameDistance);
        //    distance = Vector3.Distance(startPos, endPos);
        //    cooldown = elapsedTime / frames;
        //    //Debug.Log(distance);
        //}

        //mvtScript.startPos = startPos;
        //mvtScript.endPos = endPos;
        frontendScript.ListEndPos = frontendScript.ListEndPos.Append(endPos).ToArray();
        //mvtScript.step = vitesseMvtJoueur * Time.deltaTime;
        frontendScript.step = oneFrameDistance;
        //mvtScript.enabled = true;
    }

    /// <summary>
    /// Public funtion to allow external scripts to trigger player movement.
    /// </summary>
    public void AvancePlayer()
    {
        print("caseIncrease: " + caseIncrease + "    reverse: " + reverseArrayCheck + "    comparaison: " + comparaisonType);
        currentPos = CalculAvanceCase(currentPos, caseIncrease, trCaseList);
        //allowInput = false;
    }
}
