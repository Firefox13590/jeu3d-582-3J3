using UnityEngine;
using UnityEngine.EventSystems;

public class TestGlowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject glowObject;
    RectTransform rtrCard;
    //public RectTransform[] rtrCardList;
    //int[] objectIds;

    bool isMouseover = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(EventSystem.current);
        rtrCard = GetComponent<RectTransform>();
        //Debug.Log(glowObject.GetComponent<RectTransform>().anchoredPosition3D);
        //Debug.Log(glowObject);
        //Debug.Log(glowObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseover)
        {
            //Debug.Log("Updating glow position for: " + gameObject.name);
            glowObject.SetActive(true);
            glowObject.GetComponent<RectTransform>().anchoredPosition3D = rtrCard.anchoredPosition3D;
            glowObject.GetComponent<RectTransform>().rotation = rtrCard.rotation;
        }
        else
        {
            glowObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered: " + gameObject.name);
        isMouseover = true;
        //var spriteRenderer = GetComponent<SpriteRenderer>();
        //if (spriteRenderer != null && sprGlow != null)
        //{
        //    spriteRenderer.sprite = sprGlow;
        //}
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited: " + gameObject.name);
        isMouseover = false;
        //var spriteRenderer = GetComponent<SpriteRenderer>();
        //if (spriteRenderer != null)
        //{
        //    // Revert to original sprite or handle accordingly
        //}
    }
}
