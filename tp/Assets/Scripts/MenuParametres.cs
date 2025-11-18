using UnityEngine;
using Lib.Entities;

public class MenuParametres : MonoBehaviour
{
    public GameSettingsScriptableObject gameSettings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach(Player p in gameSettings.players)
        //{
        //    Debug.Log("Player controls: " + p.controls);
        //}

        //RegisterControlKey.OnControlKeyRegistered += UpdateControlsKey;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateControlsKey(KeyCode key)
    {
        Debug.Log("New registered key: " + key);
    }
}
