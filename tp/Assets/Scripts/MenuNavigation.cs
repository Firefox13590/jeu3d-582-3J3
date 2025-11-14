using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuParametres;
    public GameObject menuCredits;

    RectTransform rtrCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rtrCanvas = GetComponent<RectTransform>();

        Debug.Log(rtrCanvas.rect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
