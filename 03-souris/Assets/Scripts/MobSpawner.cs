using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject elephant;
    public GameObject lapin;
    public GameObject ours;

    int weight = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Reproduction", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Reproduction()
    {
        GameObject clone = null;
        weight++;

        if (weight % 2 == 0)
        {
            clone = ChoixEnnemiFaible();
        }
        else if (weight % 5 == 0)
        {
            clone = Instantiate(elephant);
        }
        else if (weight % 10 == 0)
        {
            weight = 0;
            clone = ChoixEnnemiFaible();
            clone.SetActive(true);
            clone = Instantiate(elephant);
        }

        if (clone != null)
        {
            clone.SetActive(true);
        }
    }

    GameObject ChoixEnnemiFaible()
    {
        bool isBear = Random.Range(0, 2) == 0;

        if (isBear)
        {
            return Instantiate(ours);
        }
        else
        { 
            return Instantiate(lapin);
        }
    }
}
