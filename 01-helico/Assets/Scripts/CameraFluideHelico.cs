using UnityEngine;

public class CameraFluideHelico : MonoBehaviour
{
    public GameObject cible;
    public Vector3 distance = new(2, 1, -3);

    public float amortissement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionFin = cible.transform.TransformPoint(distance);
        transform.position = Vector3.Lerp(transform.position, positionFin, amortissement);

        transform.LookAt(cible.transform);
    }
}
