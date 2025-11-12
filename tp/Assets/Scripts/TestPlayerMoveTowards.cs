using System.Linq;
using UnityEngine;

public class TestPlayerMoveTowards : MonoBehaviour
{
    public Vector3 startPos, endPos;
    public Vector3[] ListEndPos;
    public float step = 1;
    public TestCardHandler cardHandlerScript;

    TestMvtSurCases backendScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = startPos;
        backendScript = GetComponent<TestMvtSurCases>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ListEndPos.Length > 0)
        {
            endPos = ListEndPos[0];
            float distance = Vector3.Distance(transform.position, endPos);
            //Debug.Log("Current endPos: " + endPos + " | distance: " + distance);

            if (distance > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            }
            else
            {
                ListEndPos = ListEndPos[1..];
                //Debug.Log("new endPos: " + ListEndPos[0]);
            }
        }
        else
        {
            //Debug.Log("Great walk finished!");
            //backendScript.allowInput = true;
            cardHandlerScript.allowInput = true;
            cardHandlerScript.parentListeCarte.SetActive(true);
            GetComponent<TestPlayerMoveTowards>().enabled = false;
        }
    }
}
