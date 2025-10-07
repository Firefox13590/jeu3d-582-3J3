using UnityEngine;

public class ControlesDome : MonoBehaviour
{
    Animator animDome;
    AudioSource audsrcDome;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animDome = GetComponent<Animator>();
        audsrcDome = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.D))
        {
            // Debug.Log("Ctrl + D");
            animDome.SetBool("isDomeOpen", true);
        }
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.D))
        {
            // Debug.Log("Shift + D");
            animDome.SetBool("isDomeOpen", false);
        }
        //if(Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("D");
        //    //animDome.SetTrigger("ToggleDome");
        //}
    }

    public void joueSon(int num)
    {
        audsrcDome.Play();
    }
}
