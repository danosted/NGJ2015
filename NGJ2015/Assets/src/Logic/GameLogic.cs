using Assets.Backend.GameLogic;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;
using Assets.src.Logic;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
	[SerializeField] private Image Win;
	[SerializeField] private Image p1PointTimerBar;
	[SerializeField] private Image p2PointTimerBar;
	[SerializeField] private Image p1PointBar;
	[SerializeField] private Image p2PointBar;

	public bool KeyboardControls = true;

    // Use this for initialization
    void Start()
    {
        var manCol = ManagerCollection.Instance;
        manCol.EventManager.GameStarted();

        var player1 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);
		var p1 = player1.GetComponent(Enumerations.PlayerType.Player.ToString()) as Player;
		p1.Initialize(10,10,10,5);
		p1.playerName = "player1"; 
		if (KeyboardControls) {
			p1.UseKeyBoard ();
		} else {
			p1.UseGamePad1 ();
		}

		manCol.PlayerManager.Player1 = p1;
        var player2 = manCol.PlayerManager.GetNewPlayerFromType(Enumerations.PlayerType.Player);
		var p2 = player2.GetComponent(Enumerations.PlayerType.Player.ToString()) as Player;
		p2.Initialize(10,10,10,5);
		p2.playerName = "player2";
		p2.UseGamePad2();
		manCol.PlayerManager.Player2 = p2;

		var enemyLogic = EnemyLogic.Instance;    
		var hillManager = ManagerCollection.Instance.HillManager;    
		
		ManagerCollection.Instance.PlayerManager.P1PointTimerBar = p1PointTimerBar;
		ManagerCollection.Instance.PlayerManager.P2PointTimerBar = p2PointTimerBar;
		ManagerCollection.Instance.PlayerManager.P1PointBar = p1PointBar;
		ManagerCollection.Instance.PlayerManager.P2PointBar = p2PointBar;
		ManagerCollection.Instance.PlayerManager.Win = Win;

	}

}
