using UnityEngine;

public class CameraHelico : MonoBehaviour
{
    public GameObject helico;
    Vector3 posHelico;
    public float ajustX, ajustY = 3, ajustZ = -10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        posHelico = helico.transform.position;
        posHelico.x += ajustX;
        posHelico.y += ajustY;
        posHelico.z += ajustZ;

        // transform.position = posHelico;
        // transform.rotation = new Vector3();
    }
}
