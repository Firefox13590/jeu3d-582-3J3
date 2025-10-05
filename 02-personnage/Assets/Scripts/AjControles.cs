using UnityEngine;

public class AjControles : MonoBehaviour
{
    public float vitesse;
    float forceDeplacement, tourne;

    Rigidbody rbAj;
    Animator animAj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbAj = GetComponent<Rigidbody>();
        animAj = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        forceDeplacement = Input.GetAxis("Vertical") * vitesse;
        tourne = Input.GetAxis("Horizontal") * vitesse;
    }

    void FixedUpdate()
    {
        rbAj.AddRelativeForce(0, 0, forceDeplacement, ForceMode.VelocityChange);
        animAj.SetFloat("vitesse", forceDeplacement);
        transform.Rotate(0, tourne, 0);
    }
}
