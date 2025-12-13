using UnityEngine;
using Lib.Globals;
using Lib.Entities;

[CreateAssetMenu(fileName = "GameSettingsScriptableObject", menuName = "Scriptable Objects/GameSettingsScriptableObject")]
public class GameSettingsScriptableObject : ScriptableObject
{
    private Player player1 = new("Player1", new Controls(KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.LeftShift));
    private Player player2 = new("Player2", new Controls(KeyCode.I, KeyCode.L, KeyCode.K, KeyCode.J, KeyCode.Space));
    private Player player3 = new("Player3", new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return));
    private Player player4 = new("Player4", new Controls(KeyCode.Keypad8, KeyCode.Keypad6, KeyCode.Keypad2, KeyCode.Keypad4, KeyCode.Keypad5));

    //private Player[] players;
    public Player[] Players
    {
        get
        {
            return new Player[4] { player1, player2, player3, player4 };
        }
    }


    public void SetBackDefaultPlayerSettings()
    {
        player1 = new("Player1", new Controls(KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.LeftShift));
        player2 = new("Player2", new Controls(KeyCode.I, KeyCode.L, KeyCode.K, KeyCode.J, KeyCode.Space));
        player3 = new("Player3", new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return));
        player4 = new("Player4", new Controls(KeyCode.Keypad8, KeyCode.Keypad6, KeyCode.Keypad2, KeyCode.Keypad4, KeyCode.Keypad5));
    }
}
