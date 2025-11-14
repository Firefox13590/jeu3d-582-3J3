using UnityEngine;
using UnityEngine.AI;

public class Monstre : MonoBehaviour
{
    public GameObject cible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(cible.transform.position);
        GetComponent<Animator>().SetFloat("vitesse", GetComponent<NavMeshAgent>().velocity.magnitude);
    }
}
