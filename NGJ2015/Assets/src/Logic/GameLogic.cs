﻿using Assets.Backend.GameLogic;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;
using Assets.src.Logic;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    public Text p1Points;
    public Text p2Points;

    // Use this for initialization
    void Start()
    {
        var manCol = ManagerCollection.Instance;
        manCol.EventManager.GameStarted();
        var player1 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);
        var p1 = player1.GetComponent(Enumerations.PlayerType.Player.ToString()) as Player;
        //var player2 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);
        //var p2 = player2.GetComponent(Enumerations.PlayerType.Player.ToString()) as Player;
		p1.Initialize(10,10,10,5);
		var enemyLogic = EnemyLogic.Instance;

        ManagerCollection.Instance.PlayerManager.P1Text = p1Points;
        ManagerCollection.Instance.PlayerManager.P2Text = p2Points;
    }

}
