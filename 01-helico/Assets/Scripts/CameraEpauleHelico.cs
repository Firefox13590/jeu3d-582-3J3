using UnityEngine;

public class CameraEpauleHelico : MonoBehaviour
{
    public Transform cible;
    public Vector3 distance = new(2, 1, -3);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cible.position + distance;
        transform.LookAt(cible);
    }
}
