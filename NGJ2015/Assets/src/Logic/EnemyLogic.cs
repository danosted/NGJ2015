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
		private float timeToNextSpawn = 0.0f;

		public static float maxSpawnPerSecond = 10f;

		private float spawnFunction(float x) {
			float y = maxSpawnPerSecond*Mathf.Sin (0.1f * x);
			return y > 1.0f ? y : 1.0f;
		}

		void Update() {
			timeSinceLastWave += Time.deltaTime;
			if (timeSinceLastWave > timeToNextSpawn) {
				timeSinceLastWave = 0.0f;
				SpawnEnemy();
				timeToNextSpawn = GetNextSpawnTime(Time.timeSinceLevelLoad);
			}
		}

		void SpawnEnemy() {
			var enemy = ManagerCollection.Instance.EnemyManager.GetNewEnemyFromType(Enumerations.EnemyType.Enemy);
			enemy.transform.position = MathUtil.RandomOnUnitCircle() * 50f;
			var enemyScript = enemy.GetComponent(Enumerations.EnemyType.Enemy.ToString()) as Enemy;
			enemyScript.Initialize(10f,5f,5f,1f);
			var players = ManagerCollection.Instance.PlayerManager.GetActivePlayers();
			enemy.GetComponent<Enemy>().SetTarget(players);
		}

		float GetNextSpawnTime(float elapsedTime) {
			float ranNum = Random.Range (0.5f, 1.5f);

			return ranNum * (1.0f/spawnFunction (elapsedTime));
		}
    }
}
