using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Valeurs a ajuster dans l'inspecteur")]
    public GameObject[] Cameras;
    public int setCameraiIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Case.OnTileChoice += SetCameraChoixCase;

        UpdateActiveCamera();
    }

    private void OnDestroy()
    {
        Case.OnTileChoice -= SetCameraChoixCase;
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
    void SetCameraChoixCase(Transform[] _)
    {
        setCameraiIndex = 2;
        UpdateActiveCamera();
    }
}
