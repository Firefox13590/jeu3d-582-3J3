using UnityEngine;
using TMPro;
using System;
using System.Linq;

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
    }

    void InitDefaultPlayerControls()
    {
        conteneursControlesPlayer = GameObject.FindGameObjectsWithTag("ConteneurControlesPlayer");
        //conteneursControlesPlayer.OrderBy(conteneur => conteneur.name).ToArray();
        Array.Sort(conteneursControlesPlayer, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));

        for (int i = 0; i < conteneursControlesPlayer.Length; i++)
        {
            Debug.Log(conteneursControlesPlayer[i].name);
            conteneursControlesPlayer[i].GetComponentInChildren<TextMeshProUGUI>().text = gameSettings.Players[i].name;
            for (int j = 0; j < 5; j++)
            {
                conteneursControlesPlayer[i].GetComponentsInChildren<RegisterControlKey>()[j].textControle.text =
                    gameSettings.Players[i].controls.AllControls[j].ToString();
            }
        }
    }
}
