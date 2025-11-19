using System;
using TMPro;
using UnityEngine;

public class RegisterControlKey : MonoBehaviour
{
    public GameObject panelRegisterControlKey;

    bool isListeningKey = false;
    public TextMeshProUGUI textControle;
    public KeyCode registeredKey = KeyCode.None;

    public static event Action<int, string, KeyCode> OnControlKeyRegistered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        textControle = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayPanel()
    {
        panelRegisterControlKey.SetActive(true);
        isListeningKey = true;
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && isListeningKey)
        {
            if (Input.GetKeyDown(e.keyCode))
            {
                if(e.keyCode != KeyCode.Escape)
                {
                    textControle.text = e.keyCode.ToString();
                    registeredKey = e.keyCode;
                }
                Debug.Log("Detected key code: " + e.keyCode);
                isListeningKey = false;
                panelRegisterControlKey.SetActive(false);

                // decomposer le return tuple pour invoquer event
                var (indexPlayer, nomControle) = GetMissingEventParameters();
                OnControlKeyRegistered?.Invoke(indexPlayer, nomControle, e.keyCode);
            }
        }
    }

    (int, string) GetMissingEventParameters()
    {
        int indexPlayer;
        string nomControle = gameObject.name.Split('_')[1];

        indexPlayer = Int32.Parse(GetComponentsInParent<RectTransform>()[2].gameObject.name.Split('_')[1]);

        return (indexPlayer, nomControle);
    }
}
