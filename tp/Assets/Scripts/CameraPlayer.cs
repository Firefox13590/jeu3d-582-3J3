using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject[] players = new GameObject[4];
    public int playerTarget;
    public Vector3 ajustement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = players[playerTarget].transform.position + ajustement;
        transform.LookAt(players[playerTarget].transform.position);
    }
}
