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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // recuperer liste case (avec parent empty)
        trCaseList = caseList.GetComponentsInChildren<Transform>();
        // enlever premier element (parent empty) avec spread operator
        // change valeurs du array pour commencer a l'index 1 (seulement les cases)
        trCaseList = trCaseList[1..];
        Debug.Log("nb cases: " + trCaseList.Length);
        transform.position = trCaseList[currentPos].position;
        frontendScript = GetComponent<TestPlayerMoveTowards>();
        print("caseIncrease: " + caseIncrease + "    reverse: " + reverseArrayCheck);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && allowInput)
        //{
        //    currentPos = CalculAvanceCase(currentPos, caseIncrease, trCaseList);
        //    allowInput = false;
        //}
    }

    /// <summary>
    /// Calcule l'avancement du joueur sur les cases.
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="movesLeft"></param>
    /// <param name="trCaseList"></param>
    int CalculAvanceCase(int currentPos, int movesLeft, Transform[] trCaseList)
    {
        Debug.Log($"debut pre-check loop: {currentPos}\nmoves left: {movesLeft}");
        int nextPos, maxPos = trCaseList.Length - 1;
        frontendScript.startPos = trCaseList[ArrayMovement.CheckForResetLoop(currentPos, maxPos, reverse: reverseArrayCheck)].position;

        while (movesLeft > 0)
        {
            currentPos = ArrayMovement.CheckForResetLoop(currentPos, maxPos, reverse: reverseArrayCheck);
            if (reverseArrayCheck)
            {
                nextPos = currentPos + 1;
            }
            else
            {
                nextPos = currentPos - 1;
            }
            nextPos = ArrayMovement.CheckForResetLoop(nextPos, maxPos, reverse: reverseArrayCheck);
            Debug.Log($"pos 1 index:{currentPos}\npos 2 index:{nextPos}");
            MovePlayer(trCaseList[currentPos].position, trCaseList[nextPos].position, tempsMvtJoueur);
            currentPos++;
            movesLeft--;
        }

        frontendScript.enabled = true;

        return currentPos;
    }

    /// <summary>
    /// Checks whether the specified value exceeds the maximum limit and resets it if necessary.
    /// </summary>
    /// <param name="value">The value to check against the maximum limit.</param>
    /// <param name="max">The maximum allowable value.</param>
    /// <param name="resetValue">The value to return if <paramref name="value"/> exceeds <paramref name="max"/>. Defaults to 0.</param>
    /// <param name="comparaison">A character indicating the comparison operation. 'g' = '>=', 'e' = '==', 'o' = '>'.</param>
    /// <returns>The original <paramref name="value"/> if it does not exceed <paramref name="max"/>; otherwise, <paramref
    /// name="resetValue"/>.</returns>
    int CheckForResetLoop(int value, int max, int resetValue = 0, char comparaison = 'o')
    {
        //Debug.Log($"value: {value}, max: {max}");

        if (comparaison == 'e' && value == max ||
            comparaison == 'o' && value > max ||
            comparaison == 'g' && value >= max)
        {
            //Debug.Log($"reset from {value} to {resetValue}");
            return resetValue;
        }
        //Debug.Log($"no reset, value stays {value}");
        return value;
    }

    void MovePlayer(Vector3 startPos, Vector3 endPos, float elapsedTime = 1, int frames = 30)
    {
        //Debug.Log(startPos.magnitude);
        //Debug.Log(startPos.sqrMagnitude);
        //transform.position = endPos;

        float distance = Vector3.Distance(startPos, endPos);
        //float cooldown = elapsedTime / frames;
        //float oneFrameDistance = distance * cooldown;
        float oneFrameDistance = distance / (elapsedTime * frames);
        Debug.Log("distance: " + distance);
        //Debug.Log("cooldown: " + cooldown);
        Debug.Log("oneframeDistance: " + oneFrameDistance);
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

    public void AvancePlayer()
    {
        currentPos = CalculAvanceCase(currentPos, caseIncrease, trCaseList);
        allowInput = false;
    }
}
