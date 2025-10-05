using UnityEngine;

public class RotationHelice : MonoBehaviour
{
    public Vector3 vitesseRotation;
    public float vitesseMaxRotation, accelerationRotation;
    public bool enMarche;

    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vitesseRotation = new(0, 0, 0);
        vitesseMaxRotation = 100;
        accelerationRotation = .1f;
        enMarche = false;

        //audioSource = GetComponent<AudioSource>();
        audioSource = gameObject.GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // toggle etat de marche helicoptere
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enMarche = !enMarche;
            //Debug.Log(audioSource);
        }

        // gere acceleration et deceleration rotation
        if (enMarche)
        {
            if (vitesseRotation.y < vitesseMaxRotation)
            {
                vitesseRotation.y += accelerationRotation;
            }
            else
            {
                vitesseRotation.y = vitesseMaxRotation;
            }

            // gestion du son
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (vitesseRotation.y > 0)
            {
                vitesseRotation.y -= accelerationRotation;
            }
            else
            {
                vitesseRotation.y = 0;
            }
        }
        transform.Rotate(vitesseRotation, Space.Self);

        // ajuste volume et pitch selon vitesse rotation (indicateur enMarche)
        audioSource.volume = vitesseRotation.y / vitesseMaxRotation;
        audioSource.pitch = .5f + audioSource.volume * .5f;
    }
}
