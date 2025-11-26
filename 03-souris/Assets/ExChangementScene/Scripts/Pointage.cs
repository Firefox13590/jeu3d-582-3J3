using UnityEngine;
using TMPro;

public class Pointage : MonoBehaviour
{
    public static int points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = Random.Range(0, int.MaxValue);
        GetComponent<TextMeshProUGUI>().text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            points = Random.Range(0, int.MaxValue);
            GetComponent<TextMeshProUGUI>().text = points.ToString();
        }
    }
}
