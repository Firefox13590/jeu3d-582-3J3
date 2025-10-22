using UnityEngine;

public class AjControles : MonoBehaviour
{
    [Header("Mouvements")]
    public float vitesse;
    float forceDeplacement, valeurTourne, forceSaut, forceLaterale;
    public float vitesseTourne, hauteurSaut;

    [Header("Spherecast")]
    public bool auSol = true;

    Rigidbody rbAj;
    Animator animAj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbAj = GetComponent<Rigidbody>();
        animAj = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        forceDeplacement = Input.GetAxis("Vertical") * vitesse;
        forceLaterale = Input.GetAxis("Horizontal") * vitesse;
        valeurTourne = Input.GetAxis("Mouse X") * vitesseTourne;

        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            forceSaut = hauteurSaut;
            animAj.SetBool("isJumping", true);
        }

        // Physics.SphereCast();

        auSol = Physics.SphereCast(transform.position /* + new Vector3(0, .25f, 0) */, .5f, Vector3.up, out RaycastHit infoCollision);
    }

    void FixedUpdate()
    {
        rbAj.AddRelativeForce(forceLaterale, forceSaut, forceDeplacement, ForceMode.VelocityChange);
        animAj.SetFloat("vitesse", forceDeplacement);
        transform.Rotate(0, valeurTourne, 0);
        forceSaut = 0;
        animAj.SetBool("isJumping", false);
    }
}
