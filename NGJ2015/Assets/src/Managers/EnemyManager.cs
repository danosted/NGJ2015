using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers
{
    public class EnemyManager : ManagerBase
    {
        public GameObject GetNewEnemyFromType(Enumerations.EnemyType enemyType)
        {
            Debug.Log(string.Format("Fetching object with type '{0}'.", enemyType));
            if(InactiveObjects.Exists(x => x.GetComponent(enemyType.ToString())))
            {
                Debug.Log(string.Format("Object found in pool."));
                var poolObject = InactiveObjects.Find(x => x.GetComponent(enemyType.ToString()));
                var inactiveGO = GameObject.Instantiate(poolObject) as GameObject;
                ActiveObjects.Add(inactiveGO.gameObject);
                inactiveGO.transform.parent = transform;
                inactiveGO.SetActive(true);
                return inactiveGO;
            }
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
