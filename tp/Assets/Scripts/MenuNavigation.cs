using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public RectTransform[] listeMenus;

    RectTransform rtrCanvas;
    int menuActuel = 1, menuCible = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rtrCanvas = GetComponent<RectTransform>();
        Debug.Log(rtrCanvas.rect);

        for(int i = 0; i < listeMenus.Length; i++)
        {
            listeMenus[i].sizeDelta = new Vector2(rtrCanvas.rect.width, rtrCanvas.rect.height);
            listeMenus[i].anchoredPosition = new Vector2(rtrCanvas.rect.width * (i - 1), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu(int targetMenuIndex)
    {
        menuCible = targetMenuIndex;
        Vector2 movement = listeMenus[menuActuel].anchoredPosition - listeMenus[menuCible].anchoredPosition;
        Debug.Log("movement: " + movement);

        foreach (RectTransform menu in listeMenus)
        {
            menu.anchoredPosition += movement;
        }
        menuActuel = menuCible;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
