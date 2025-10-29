using UnityEngine;

public class TestCardMvt : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 targetPos;
    //public bool moveCard = false;
    public float step = .1f;

    RectTransform rtrSelf;
    float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rtrSelf = GetComponent<RectTransform>();
        rtrSelf.anchoredPosition = startPos;
        distance = Vector2.Distance(rtrSelf.anchoredPosition, targetPos);
        //Debug.Log(distance);
    }

    // Update is called once per frame
    void Update()
    {
        if(distance > 0)
        {
            rtrSelf.anchoredPosition = Vector2.MoveTowards(rtrSelf.anchoredPosition, targetPos, step);
            distance = Vector2.Distance(rtrSelf.anchoredPosition, targetPos);
        }
        else
        {
            GetComponent<TestCardMvt>().enabled = false;
        }
    }
}
