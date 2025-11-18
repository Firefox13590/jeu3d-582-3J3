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

                //OnControlKeyRegistered?.Invoke(registeredKey);
            }
        }
    }

    (int, string, KeyCode) GetEventParameters()
    {
        return (0, gameObject.name, registeredKey);
    }
}
