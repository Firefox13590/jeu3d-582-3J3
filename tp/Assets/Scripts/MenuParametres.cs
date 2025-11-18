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

        RegisterControlKey.OnControlKeyRegistered += UpdateControlsKey;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateControlsKey(int indexPlayer, string nomControle, KeyCode key)
    {
        Debug.Log($"Event output (int, string, KeyCode): {indexPlayer}    {nomControle}    {key}");
    }
}
