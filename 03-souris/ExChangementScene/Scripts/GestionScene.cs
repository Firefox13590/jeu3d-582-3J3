using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour
{
    [SerializeField]
    string nomScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //nomScene = SceneManager.GetActiveScene().name;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nomScene);
        }
    }
}
