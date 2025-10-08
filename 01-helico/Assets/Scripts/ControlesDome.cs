using UnityEngine;

public class ControlesDome : MonoBehaviour
{
    Animator animDome;
    AudioSource audsrcDome;
    public float vitesseSon = 3 / 5.696f;
    //public AudioClip sonDome;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animDome = GetComponent<Animator>();
        audsrcDome = GetComponent<AudioSource>();
        //audsrcDome.pitch = vitesseSon;
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

    /// <summary>
    /// Joue le son du dome.
    /// </summary>
    /// <param name="soundDirection">Direction du son (vitesse). 1 vitesse normale. -1 vitesse inverse.</param>
    public void JoueSon(int soundDirection)
    {
        Debug.Log(soundDirection);

        audsrcDome.pitch = vitesseSon * soundDirection;

        // repositionne le point de lecture au debut ou a al fin selon la direction du son
        if (soundDirection == -1)
        {
            // repositionne tete de lecture vers la fin pour que le pitch puisse jouer inversement
            // puisque le clip a un moment de silenece vers la fin, repositionnement un peu avant pour eviter silence
            //audsrcDome.timeSamples = Mathf.Max(1, audsrcDome.clip.samples - 1);
            audsrcDome.timeSamples = Mathf.Max(1, audsrcDome.clip.samples - (int)(audsrcDome.clip.samples * (vitesseSon / 1.25f)));
        }
        else
        {
            audsrcDome.timeSamples = 0;
        }
        //Debug.Log(audsrcDome.timeSamples);

        audsrcDome.Play();
    }
}
