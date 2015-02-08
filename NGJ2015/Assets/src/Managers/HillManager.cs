using System.Collections.Generic;
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
		private bool playerGainingPoints;

		private GameObject carpetObject;
		void Start() 
		{
			carpetObject = transform.GetChild(0).gameObject;
			playerGainingPoints = false;
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
				Vector3 modifiedPlayerPosition = new Vector3(player.transform.position.x, player.transform.position.y*3, player.transform.position.z);
				if (kings.Contains(player))
				{
					if (Vector3.Magnitude(modifiedPlayerPosition) > hillRadius || player.IsDead())
					{
						kings.Remove (player);
					}
				}
				else 
				{
                    if (Vector3.Magnitude(modifiedPlayerPosition) <= hillRadius && !player.IsDead())
					{
						kings.Add (player);
					}
				}
			}
			int enemyKings = 0;
			foreach(GameObject enemy in enemies)
			{
				if (Vector3.Magnitude(new Vector3(enemy.transform.position.x, enemy.transform.position.y*3, enemy.transform.position.z)) <= hillRadius)
					enemyKings++;
			}
			if (kings.Count == 1 && enemyKings == 0 && !playerGainingPoints)
			{
				Player player = kings[0].GetComponent<Player>();
				if (player != null)
				{
					ManagerCollection.Instance.PlayerManager.StartGivingPointsToPlayer(player);
					playerGainingPoints = true;
					iTween.ColorTo(carpetObject, ManagerCollection.Instance.PlayerManager.GetPlayerColor(player) + new Color(0.001f, 0.001f, 0.001f), 1f);
				}
			}
			else if (playerGainingPoints && !(kings.Count == 1 && enemyKings == 0))
			{
				ManagerCollection.Instance.PlayerManager.StopGivingPointsToPlayer();
				iTween.ColorTo(carpetObject, Color.white, 1f);
				playerGainingPoints = false;
			}
		}
	}
}
