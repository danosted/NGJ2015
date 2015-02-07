using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers
{
    public class PowerupManager : ManagerBase
    {
        public GameObject GetNewEnemyFromType(Enumerations.EnemyType enemyType)
        {
            Debug.Log(string.Format("Fetching object with type '{0}'.", enemyType), gameObject);
            var GO = PrefabPool.Find(x => x.GetComponent(Enumerations.PowerupType.DamagePowerup.ToString()) != null);
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
    }
}
