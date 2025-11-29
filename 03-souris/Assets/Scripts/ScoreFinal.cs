using TMPro;
using UnityEngine;

public class ScoreFinal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Tu as {DeplacementPersoScript.pointage} points.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
