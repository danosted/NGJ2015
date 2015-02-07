using Assets.src.Common;
using Assets.src.Managers;
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var manCol = ManagerCollection.Instance;
        manCol.EventManager.GameStarted();
        var player1 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);
        var player2 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);



    }

}
