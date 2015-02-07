﻿using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Assets.Backend.Utililties;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Managers
{
	public class HillManager : MonoBehaviour
	{
		[SerializeField] private float hillRadius = 10;
		private List<Player> kings = new List<Player>();

		private List<GameObject> enemies;
		private List<GameObject> players;

		void Start() 
		{
			enemies = ManagerCollection.Instance.EnemyManager.GetActiveMonsters();
			players = ManagerCollection.Instance.PlayerManager.GetActivePlayers();
			foreach(GameObject player in players)
			{
				kings.Add(player.GetComponent<Player>());
			}

		}

		void FixedUpdate() 
		{
			foreach(GameObject playerObject in players)
			{
				Player player = playerObject.GetComponent<Player>();
				if (kings.Contains(player))
				{
					if (Vector3.Magnitude(player.transform.position) > hillRadius)
					{
						kings.Remove (player);
					}
				}
				else 
				{
					if (Vector3.Magnitude(player.transform.position) <= hillRadius)
					{
						kings.Add (player);
					}
				}
			}
			int enemyKings = 0;
			foreach(GameObject enemy in enemies)
			{
				if (Vector3.Magnitude(enemy.transform.position) <= hillRadius)
					enemyKings++;
			}
			if (kings.Count == 1 && enemyKings == 0)
			{
				Player player = kings[0].GetComponent<Player>();
				if (player != null)
				{
					ManagerCollection.Instance.PlayerManager.GivePointsToPlayer(player, 1);
				}
			}
		}
	}
}