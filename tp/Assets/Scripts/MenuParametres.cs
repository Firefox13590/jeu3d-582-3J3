using UnityEngine;
using Lib.Entities;

public class MenuParametres : MonoBehaviour
{
    public GameSettingsScriptableObject gameSettings;

    GameObject[] conteneursControlesPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach(Player p in gameSettings.Players)
        //{
        //    Debug.Log("Player controls: " + p.controls);
        //}

        conteneursControlesPlayer = GameObject.FindGameObjectsWithTag("ConteneurControlesPlayer");
        for (int i = 0; i < conteneursControlesPlayer.Length; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                conteneursControlesPlayer[i].GetComponentsInChildren<RegisterControlKey>()[j].textControle.text =
                    gameSettings.Players[i].controls.AllControls[j].ToString();
            }
        }

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
}
