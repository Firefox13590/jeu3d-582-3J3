using System;
using Random = UnityEngine.Random;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MouvementsHelicoptere : MonoBehaviour
{
    public float
    vitesseHorizontale, vitesseVerticale, vitesseAvant,
    essenceActuelle, essenceMax = 100;
    float forceHorizontale, forceAvant;
    bool enMarche, volumeGlobalSourdine;
    public GameObject helice, explosion, messageAlerteEssence;
    Rigidbody rbHelico;
    AudioSource audioHelico;
    public AudioClip sonBidon, sonExplosion;
    public Image barreEssence;
    Color messageAlerteCouleur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vitesseHorizontale = 20;
        vitesseVerticale = 15;
        vitesseAvant = 10;

        rbHelico = GetComponent<Rigidbody>();
        audioHelico = GetComponent<AudioSource>();

        essenceActuelle = essenceMax;

        messageAlerteCouleur = messageAlerteEssence.GetComponent<TMP_Text>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        enMarche = helice.GetComponent<RotationHelice>().enMarche;

        // mvt helico autorise seulement si enMarche
        if (enMarche)
        {
            // rbHelico.useGravity = false;

            // calcul forces pour mvt helico
            forceHorizontale = Input.GetAxis("Horizontal") * vitesseHorizontale;
            forceAvant = Input.GetAxis("Vertical") * vitesseAvant;
            // Debug.Log(forceAvant);

            // force horizontale
            rbHelico.AddRelativeTorque(0, forceHorizontale, 0);

            // force verticale
            if (Input.GetKey(KeyCode.Space))
            {
                rbHelico.AddRelativeForce(0, vitesseVerticale, 0);
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                rbHelico.AddRelativeForce(0, 0, forceAvant);
            }

            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            // Debug.Log(rbHelico.GetAccumulatedForce());
            // Debug.Log(rbHelico.linearVelocity.magnitude);

            // gestion de l'essence
            essenceActuelle -= .1f;
            float pourcentage = essenceActuelle / essenceMax;
            barreEssence.fillAmount = pourcentage;
            if (barreEssence.fillAmount <= 0)
            {
                KaboomHelicoptere();
            }
            else if (barreEssence.fillAmount < .3f)
            {
                // message d'alerte
                messageAlerteEssence.SetActive(true);
            }
            else
            {
                // masquer alerte
                messageAlerteEssence.SetActive(false);
                messageAlerteCouleur = new Color(1, 0, 0);
            }
        }
        else
        {
            // rbHelico.useGravity = true;
        }
        // Debug.Log(rbHelico.linearVelocity.magnitude);

        // controle sourdine
        if (Input.GetKeyDown(KeyCode.M))
        {
            volumeGlobalSourdine = !volumeGlobalSourdine;
            AudioListener.pause = volumeGlobalSourdine;
        }
    }

    void OnTriggerEnter(Collider otherObject)
    {
        // collision avec bidon
        if (otherObject.gameObject.tag == "bidon")
        {
            // detruit bidon, joue son collecte et rempli essence
            Destroy(otherObject.gameObject);
            audioHelico.PlayOneShot(sonBidon);
            essenceActuelle = essenceMax;
        }
    }

    void OnCollisionEnter(Collision collidedObject)
    {
        // si vitesse lente, helico kaboom pas
        // sinon kaboom
        float vitesseDeplacement = rbHelico.linearVelocity.magnitude;
        //Debug.Log(vitesseDeplacement);

        if (collidedObject.gameObject.tag == "decor" && vitesseDeplacement > 30)
        {
            KaboomHelicoptere();
        }
        if (collidedObject.gameObject.tag == "dome" || collidedObject.gameObject.tag == "drone")
        {
            KaboomHelicoptere();
        }
    }

    public void KaboomHelicoptere()
    {
        // activer particules explosion
        explosion.SetActive(true);

        // jouer son explosion
        audioHelico.PlayOneShot(sonExplosion);

        // desactiver control helico
        helice.GetComponent<RotationHelice>().enMarche = false;

        // go full ragdoll
        rbHelico.mass = .1f;
        rbHelico.useGravity = true;
        rbHelico.linearDamping = 0;
        rbHelico.angularDamping = 0;
        rbHelico.constraints = RigidbodyConstraints.None;

        // bonus: changer couleur du materiel
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));

        // desactiver message d'alerte
        messageAlerteEssence.SetActive(false);

        // relancer la scene
        Invoke("RelancerPartie", 8);
    }

    // recharger scene 8 sec apres explosion helico
    void RelancerPartie()
    {
        Scene sceneActuelle = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneActuelle.name);
    }
}
