using UnityEngine;
using Lib;
using Lib.Globals;
using Lib.Entities;

[CreateAssetMenu(fileName = "GameSettingsScriptableObject", menuName = "Scriptable Objects/GameSettingsScriptableObject")]
public class GameSettingsScriptableObject : ScriptableObject
{
    Player player1 = new();
    Player player2 = new();
    Player player3 = new();
    Player player4 = new();

    public Player[] players
    {
        get
        {
            return new Player[4] { player1, player2, player3, player4 };
        }
    }
}
