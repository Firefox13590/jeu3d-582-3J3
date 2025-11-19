using UnityEngine;
using Lib.Globals;
using Lib.Entities;

[CreateAssetMenu(fileName = "GameSettingsScriptableObject", menuName = "Scriptable Objects/GameSettingsScriptableObject")]
public class GameSettingsScriptableObject : ScriptableObject
{
    public Player player1 = new("Player 1", new Controls(KeyCode.W, KeyCode.D, KeyCode.S, KeyCode.A, KeyCode.LeftShift));
    public Player player2 = new("Player 2", new Controls(KeyCode.I, KeyCode.L, KeyCode.K, KeyCode.J, KeyCode.Space));
    public Player player3 = new("Player 3", new Controls(KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.Return));
    public Player player4 = new("Player 4", new Controls(KeyCode.Keypad8, KeyCode.Keypad6, KeyCode.Keypad2, KeyCode.Keypad4, KeyCode.Keypad5));

    public Player[] Players
    {
        get
        {
            return new Player[4] { player1, player2, player3, player4 };
        }
    }
}
