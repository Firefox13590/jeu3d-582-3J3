using UnityEngine;
using Lib;
using Lib.Globals;
using Lib.Entities;

[CreateAssetMenu(fileName = "GameSettingsScriptableObject", menuName = "Scriptable Objects/GameSettingsScriptableObject")]
public class GameSettingsScriptableObject : ScriptableObject
{
    public Player player1 = new();
    public Player player2 = new();
    public Player player3 = new();
    public Player player4 = new();

    public Player[] players
    {
        get
        {
            return new Player[4] { player1, player2, player3, player4 };
        }
    }
}
