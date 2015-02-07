using System.Collections.Generic;
using Assets.Backend.GameLogic;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.src.Managers.Entities;

public class PlayerManager : ManagerBase
{
    public Text P1Text;
    public Text P2Text;

    public GameObject GetNewPlayerFromType(Enumerations.PlayerType playerType)
    {
        Debug.Log(string.Format("Fetching object with type '{0}'.", playerType));
        var GO = PrefabPool.Find(x => x.GetComponent(playerType.ToString()) != null);
        if (GO == null)
        {
            var msg = string.Format("No object found with type '{0}'.", playerType);
            Debug.LogError(msg, gameObject);
        }
        var resultGO = GameObject.Instantiate(GO) as GameObject;
        ActiveObjects.Add(resultGO.gameObject);
        resultGO.transform.parent = transform;
        return resultGO;
    }

	public Player Player1{ get; set; } 
	public Player Player2{ get; set; } 

    public List<GameObject> GetActivePlayers()
    {
        //Debug.Log(string.Format("Fetching active player object."));
        return ActiveObjects;
    }

    public void GivePointsToPlayer(Player player, long points)
    {
        player.AddPoints(points);
		switch (player.playerName)
        {
            case "player1":
                P1Text.text = player.GetPoints().ToString();
                break;
            case "player2":
                P2Text.text = player.GetPoints().ToString();
                break;

        }
    }
}
