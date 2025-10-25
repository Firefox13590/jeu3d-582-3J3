using UnityEngine;

public class TestPlayerMoveTowards : MonoBehaviour
{
    public Vector3 startPos, endPos;
    public Vector3[] ListEndPos;
    public float step = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, endPos);
        if (distance > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
        }
        else
        {
            GetComponent<TestPlayerMoveTowards>().enabled = false;
        }

        //foreach (Vector3 dynamicEndPos in ListEndPos)
        //{
        //    float distance = Vector3.Distance(transform.position, dynamicEndPos);
        //    while (distance > 0)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, dynamicEndPos, step);
        //        distance = Vector3.Distance(transform.position, dynamicEndPos);
        //    }
        //    transform.position = dynamicEndPos;
        //}
        //GetComponent<TestPlayerMoveTowards>().enabled = false;
    }
}
