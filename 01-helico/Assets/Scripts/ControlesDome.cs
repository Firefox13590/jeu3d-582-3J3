using UnityEngine;

public class ControlesDome : MonoBehaviour
{
    Animator animDome;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animDome = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Ctrl + D");
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Shift + D");
        }
        //if(Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("D");
        //    //animDome.SetTrigger("ToggleDome");
        //}
    }
}
