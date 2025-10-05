using UnityEngine;

public class DemarrerJeu : MonoBehaviour
{
    public GameObject[] objetsAActiver;
    public GameObject[] objetsADesactiver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CommencerJeu()
    {
        foreach (GameObject obj in objetsAActiver)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objetsADesactiver)
        {
            obj.SetActive(false);
        }
    }
}
