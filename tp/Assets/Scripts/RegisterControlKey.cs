using System;
using TMPro;
using UnityEngine;

public class RegisterControlKey : MonoBehaviour
{
    public GameObject panelRegisterControlKey;

    bool isListeningKey = false;
    TextMeshProUGUI textControle;
    public KeyCode registeredKey = KeyCode.None;

    public static event Action<int, string, KeyCode> OnControlKeyRegistered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textControle = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(isListeningKey)
        //{
        //    if(Input.anyKeyDown)
        //    {
        //        Debug.Log(Input.inputString);
        //        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        //        {
        //            if(Input.GetKeyDown(kcode))
        //            {
        //                textControle.text = kcode.ToString();
        //                isListeningKey = false;
        //                panelRegisterControlKey.SetActive(false);
        //            }
        //        }
        //    }
        //}
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
                var (arg0, arg1) = GetMissingEventParameters();
                OnControlKeyRegistered?.Invoke(arg0, arg1, e.keyCode);
            }
        }
    }

    (int, string) GetMissingEventParameters()
    {
        int indexPlayer;
        string nomControle = gameObject.name.Split('_')[1];

        indexPlayer = 0;
        Debug.Log(GetComponentsInParent<RectTransform>()[2].gameObject.name.Split('_')[1]);

        return (indexPlayer, nomControle);
    }
}
