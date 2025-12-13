using UnityEngine;
using TMPro;

public class PointsFin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Pointage\n final:\n" + Pointage.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
