using UnityEngine;
using UnityEngine.AI;

public class Monstre : MonoBehaviour
{
    public GameObject cible;
    public AudioClip sonMort, sonBlesse;
    public int maxHp = 1;
    int hp;
    public DeplacementPersoScript scriptPerso;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(cible.transform.position);
        GetComponent<Animator>().SetFloat("vitesse", GetComponent<NavMeshAgent>().velocity.magnitude);

        if (!GestionScenes.isInGame)
        {
            GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            GetComponent<NavMeshAgent>().speed = 0;
        }
    }

    public void Touche()
    {
        hp--;
        if(hp > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(sonBlesse);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            GetComponent<Animator>().SetBool("isDead", true);
            GetComponent<NavMeshAgent>().speed = 0;
            gameObject.tag = "Untagged";

            DeplacementPersoScript.pointage += maxHp;
            scriptPerso.textePointage.text = DeplacementPersoScript.pointage.ToString();

            Destroy(gameObject, 2);
        }
    }
}
