using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScenes : MonoBehaviour
{
    public AudioSource musique;
    public static bool isInGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(musique.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isInGame)
            {
                musique.Play();
                isInGame = true;
                DeplacementPersoScript.pointage = 0;

                SceneManager.LoadScene(1);
            }
        }
    }
}
