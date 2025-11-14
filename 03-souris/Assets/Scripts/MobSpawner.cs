using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject elephant;
    public GameObject lapin;
    public GameObject ours;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Reproduction", 5, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reproduction()
    {
        GameObject clone;
        clone = Instantiate(elephant);
        clone.SetActive(true);
    }
}
