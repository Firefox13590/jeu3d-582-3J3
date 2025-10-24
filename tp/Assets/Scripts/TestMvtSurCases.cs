using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class TestMvtSurCases : MonoBehaviour
{
    public GameObject caseList, player;
    public int caseIncrease = 5;
    public float vitesseMvtJoueur = 1;

    Transform[] trCaseList;
    int currentPos = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // recuperer liste case (avec parent empty)
        trCaseList = caseList.GetComponentsInChildren<Transform>();
        // enlever premier element (parent empty)
        trCaseList = trCaseList.Skip(1).ToArray();
        Debug.Log("nb cases: " + trCaseList.Length);
        player.transform.position = trCaseList[currentPos].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPos = CalculAvanceCase(currentPos, caseIncrease, trCaseList);
        }
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
        int maxPos = trCaseList.Length - 1;

        //if (destPos > lastPos)
        //{
        //    destPos -= lastPos;
        //}

        while (movesLeft > 0)
        {
            currentPos = CheckForResetLoop(currentPos, maxPos);
            int nextPos = currentPos + 1;
            nextPos = CheckForResetLoop(nextPos, maxPos/*, comparaison: 'g'*/);
            Debug.Log($"pos 1 index:{currentPos}\npos 2 index:{nextPos}");
            MovePlayer(trCaseList[currentPos].position, trCaseList[nextPos].position);
            currentPos++;
            movesLeft--;
        }

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

    void MovePlayer(Vector3 startPos, Vector3 endPos)
    {
        player.transform.position = endPos;
    }
}
