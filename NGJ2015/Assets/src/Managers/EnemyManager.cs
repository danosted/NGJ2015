using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Backend.DataAccess.BaseClasses;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers
{
    public class EnemyManager : ManagerBase
    {
        public GameObject GetNewEnemyFromType(Enumerations.EnemyType enemyType)
        {
            Debug.Log(string.Format("Fetching object with type '{0}'.", enemyType));
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

    }
}
