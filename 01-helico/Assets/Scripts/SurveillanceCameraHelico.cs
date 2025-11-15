using UnityEngine;

public class SurveillanceCameraHelico : MonoBehaviour
{
    public GameObject cible;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cible.transform);
    }
}
