using System.Collections.Generic;
using Assets.src.Common;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Managers
{
    public class EnemyManager : ManagerBase
    {
        public GameObject GetNewEnemyFromType(Enumerations.EnemyType enemyType)
        {
            if(InactiveObjects.Exists(x => x.GetComponent(enemyType.ToString())))
            {
                var poolObject = InactiveObjects.Find(x => x.GetComponent(enemyType.ToString()));
				ActiveObjects.Add(poolObject);
				InactiveObjects.Remove(poolObject);
				poolObject.transform.parent = transform;
				Enemy pooledEnemy = poolObject.GetComponent<Enemy>();
				pooledEnemy.Initialize(pooledEnemy.GetHealth(), pooledEnemy.GetSpeed(), pooledEnemy.GetRange(), pooledEnemy.GetDamage());
				poolObject.SetActive(true);
				return poolObject;
            }
            //Debug.LogWarning(enemyType);
            var GO = PrefabPool.Find(x => x.GetComponent(enemyType.ToString()) != null);
            if (GO == null)
            {
                var msg = string.Format("No object found with type '{0}'.", enemyType);
                Debug.LogError(msg, gameObject);
            }
            var resultGO = GameObject.Instantiate(GO) as GameObject;
            ActiveObjects.Add(resultGO.gameObject);
            resultGO.transform.parent = transform;
            return resultGO;
        }

        public List<GameObject> GetActiveMonsters()
        {
            //Debug.Log(string.Format("Fetching active monster objects."));
            return ActiveObjects;
        }

        public void PoolEnemyObject(GameObject enemyGameObject)
        {
            enemyGameObject.SetActive(false);
            ActiveObjects.Remove(enemyGameObject);
            InactiveObjects.Add(enemyGameObject);
        }
    }
}
