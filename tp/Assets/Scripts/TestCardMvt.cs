using UnityEngine;

public class TestCardMvt : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 targetPos;
    //public bool moveCard = false;
    public float step = 1;

    RectTransform rtrSelf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rtrSelf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
