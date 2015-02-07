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
		private int maxNumOfEnemies = 20;


		private static EnemyLogic _instance;
		
		private static object _lock = new object();

		private static bool applicationIsQuitting = false;
		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}

		public static EnemyLogic Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					Debug.LogWarning("[Singleton] Instance '" + typeof(EnemyLogic) +
					                 "' already destroyed on application quit." +
					                 " Won't create again - returning null.");
					return null;
				}
				
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (EnemyLogic)FindObjectOfType(typeof(EnemyLogic));
						
						if (FindObjectsOfType(typeof(EnemyLogic)).Length > 1)
						{
							Debug.LogError("[Singleton] Something went really wrong " +
							               " - there should never be more than 1 singleton!" +
							               " Reopening the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<EnemyLogic>();
							singleton.name = "(singleton) " + typeof(EnemyLogic).ToString();
							
							DontDestroyOnLoad(singleton);
							
							Debug.Log("[Singleton] An instance of " + typeof(EnemyLogic) +
							          " is needed in the scene, so '" + singleton +
							          "' was created with DontDestroyOnLoad.");
						}
						else
						{
							Debug.Log("[Singleton] Using instance already created: " +
							          _instance.gameObject.name);
						}
					}
					
					return _instance;
				}
			}
		}

		public float maxSpawnPerSecond = 5f;

		private float spawnFunction(float x) {
			float y = maxSpawnPerSecond*Mathf.Sin (0.1f * x);
			return y > 1.0f ? y : 1.0f;
		}

		void Update() {
			timeSinceLastWave += Time.deltaTime;
			if (timeSinceLastWave > timeToNextSpawn && ManagerCollection.Instance.EnemyManager.ActiveObjects.Count < maxNumOfEnemies) {
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
			enemy.GetComponent<Enemy>().SetTargets(players);
		}

		float GetNextSpawnTime(float elapsedTime) {
			float ranNum = Random.Range (0.5f, 1.5f);

			return ranNum * (1.0f/spawnFunction (elapsedTime));
		}
    }
}
