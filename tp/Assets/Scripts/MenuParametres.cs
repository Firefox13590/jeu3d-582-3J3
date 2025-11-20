using UnityEngine;
using TMPro;
using System;
using System.Reflection;
using Lib.Globals;

public class MenuParametres : MonoBehaviour
{
    public GameSettingsScriptableObject gameSettings;

    GameObject[] conteneursControlesPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitDefaultPlayerControls();

        RegisterControlKey.OnControlKeyRegistered += UpdateControlsKey;
    }

    void OnDestroy()
    {
        RegisterControlKey.OnControlKeyRegistered -= UpdateControlsKey;
    }



    void UpdateControlsKey(int indexPlayer, string nomControle, KeyCode key)
    {
        Debug.Log($"Event output (int, string, KeyCode): {indexPlayer}    {nomControle}    {key}");

        Type typePlayer = typeof(Controls);
        PropertyInfo propinfoControle = typePlayer.GetProperty(nomControle);
        Debug.Log($"Type: {typePlayer}    PropertyInfo: {propinfoControle.Name}");
        if (propinfoControle != null )
        {
            propinfoControle.SetValue(gameSettings.Players[indexPlayer].Controls, key);
        }
        else
        {
            Debug.Log("Cant set value");
        }
    }

    void InitDefaultPlayerControls()
    {
        //Debug.Log(gameSettings.Players.Length);

        conteneursControlesPlayer = GameObject.FindGameObjectsWithTag("ConteneurControlesPlayer");
        //conteneursControlesPlayer.OrderBy(conteneur => conteneur.name).ToArray();
        Array.Sort(conteneursControlesPlayer, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));

        for (int i = 0; i < conteneursControlesPlayer.Length; i++)
        {
            //Debug.Log(conteneursControlesPlayer[i].name);
            conteneursControlesPlayer[i].GetComponentInChildren<TextMeshProUGUI>().text = gameSettings.Players[i].Name;
            for (int j = 0; j < 5; j++)
            {
                conteneursControlesPlayer[i].GetComponentsInChildren<RegisterControlKey>()[j].textControle.text =
                    gameSettings.Players[i].Controls.AllControls[j].ToString();
            }
        }
    }
}
