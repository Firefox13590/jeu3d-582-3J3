using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameObject[] Cameras;
    public int setCameraiIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Case.OnTileChoiceStart += SetCameraChoixCase;

        UpdateActiveCamera();
    }

    private void OnDestroy()
    {
        Case.OnTileChoiceStart -= SetCameraChoixCase;
    }



    void UpdateActiveCamera()
    {
        for(int i = 0; i< Cameras.Length; i++)
        {
            if(i == setCameraiIndex)
            {
                Cameras[i].SetActive(true);
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }

    void SetCameraMap()
    {
        setCameraiIndex = 0;
        UpdateActiveCamera();
    }
    void SetCameraPlayer()
    {
        setCameraiIndex = 1;
        UpdateActiveCamera();
    }
    void SetCameraChoixCase(Transform[] options)
    {
        setCameraiIndex = 2;
        //Cameras[2].transform.position = options[0].position + options[1].position + new Vector3(-5, 10);
        Cameras[2].transform.position = Vector3.Lerp(options[0].position, options[1].position, .5f) + new Vector3(-5, 10);
        UpdateActiveCamera();
    }
}
