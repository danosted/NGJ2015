using System.Collections.Generic;
using Assets.Backend.GameLogic;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.src.Managers.Entities;
using Assets.src.Common;

public class PlayerManager : ManagerBase
{	
	public Image Win;
	public Image P1PointTimerBar;
	public Image P2PointTimerBar;
	public Image P1PointBar;
	public Image P2PointBar;

	private Player _gainingPlayer;
	[SerializeField] private int pointsPerInterval = 25;
	[SerializeField] private int pointsToWin = 300;
	[SerializeField] private float pointInterval = 2f;
	

	private float lastPointTick;

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

	void FixedUpdate()
	{
		if (_gainingPlayer != null)
		{
			float timeDiff = (Time.time - lastPointTick) / pointInterval;
			switch (_gainingPlayer.playerName)
			{
			case "player1":
				P1PointTimerBar.fillAmount = (timeDiff < 1 ? timeDiff : 1);
				break;
			case "player2":
				P2PointTimerBar.fillAmount = (timeDiff < 1 ? timeDiff : 1);
				break;
			}
		}
	}
	
	public Player Player1{ get; set; } 
	public Player Player2{ get; set; } 

    public List<GameObject> GetActivePlayers()
    {
        return ActiveObjects;
    }

	public void StartGivingPointsToPlayer(Player player)
	{
		StartCoroutine(GivePointsToPlayer(player, pointsPerInterval));
	}
	public void StopGivingPointsToPlayer()
	{
		StopAllCoroutines();
		P1PointTimerBar.fillAmount = 0;
		P2PointTimerBar.fillAmount = 0;
		_gainingPlayer = null;
	}

	public Color GetPlayerColor(Player player)
	{
		if (player.playerName == "player1")
			return new Color(185f/255f,71f/255f,177f/255f);
		return new Color(96f/255f,207f/255f,135f/255f);
	}

	public IEnumerator GivePointsToPlayer(Player player, long points)
    {
        while(true)
		{
			_gainingPlayer = player;
			lastPointTick = Time.time;
			yield return new WaitForSeconds(pointInterval);
			player.AddPoints(points);
			float playerPoints = (float)player.GetPoints();
			bool isWon = false;
			if (playerPoints >= pointsToWin)
			{
				Win.transform.GetChild(0).GetComponent<Text>().text = player.playerName;

				Win.transform.GetChild(1).GetComponent<Text>().text = player.playerName;
				Color playerColor = GetPlayerColor(player);
				Win.transform.GetChild(0).GetComponent<Text>().color = playerColor;
				Win.transform.GetChild(2).GetComponent<Text>().color = playerColor;

				Win.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			switch (player.playerName)
	        {
	            case "player1":
					P1PointBar.fillAmount = playerPoints/pointsToWin;
					break;
	            case "player2":
					P2PointBar.fillAmount = playerPoints/pointsToWin;
					break;
			}
		}
    }
}
