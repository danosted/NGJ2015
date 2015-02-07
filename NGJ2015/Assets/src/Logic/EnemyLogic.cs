using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Assets.Backend.Utililties;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Logic
{
    public class EnemyLogic : MonoBehaviour
	{
		private float timeSinceLastWave = 0.0f;
		private float timeBetweenWaves = 5.0f;
		private float difficulty = 1.0f;

		void Update() {
			timeSinceLastWave += Time.deltaTime;
			if (timeSinceLastWave > timeBetweenWaves) {
				timeSinceLastWave = 0.0f;
				StartEnemyWave(20, difficulty);
				difficulty += 1.0f;
			}
		}

		public void StartEnemyWave(int EnemyCount, float difficulty)
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                var enemy = ManagerCollection.Instance.EnemyManager.GetNewEnemyFromType(Enumerations.EnemyType.Enemy);
                enemy.transform.position = MathUtil.RandomOnUnitCircle() * 50f;
                var enemyScript = enemy.GetComponent(Enumerations.EnemyType.Enemy.ToString()) as Enemy;
                enemyScript.Initialize(10f,5f,5f,1f);
                var players = ManagerCollection.Instance.PlayerManager.GetActivePlayers();
                enemy.GetComponent<Enemy>().SetTarget(players);
            }

			// Start next wave
        }
    }
}
