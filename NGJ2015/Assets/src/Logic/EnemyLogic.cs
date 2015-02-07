using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Backend.Utililties;
using Assets.src.Common;
using Assets.src.Managers;
using Assets.src.Managers.Entities;
using UnityEngine;

namespace Assets.src.Logic
{
    public class EnemyLogic
    {
        public void StartEnemyWave(int EnemyCount, float difficulty)
        {
            for (int i = 0; i < EnemyCount; i++)
            {
                var enemy = ManagerCollection.Instance.EnemyManager.GetNewEnemyFromType(Enumerations.EnemyType.Enemy);
                enemy.transform.position = MathUtil.RandomOnUnitCircle() * 50f;
                var enemyScript = enemy.GetComponent(Enumerations.EnemyType.Enemy.ToString()) as Enemy;
                enemyScript.Initialize(10f,5f,5f,1f);
                var players = ManagerCollection.Instance.PlayerManager.GetActivePlayers();
                var nearestPlayer = GetNearestTarget(players, enemy.transform.position);
                enemy.GetComponent<Enemy>().SetTarget(nearestPlayer);
            }
        }

        public GameObject GetNearestTarget(List<GameObject> targets, Vector3 position)
        {
            var nearestDist = float.MaxValue;
            GameObject result = targets[0];
            foreach (var target in targets)
            {
                var tempDist = Vector3.Distance(target.transform.position, position);
                if (tempDist < nearestDist)
                {
                    nearestDist = tempDist;
                    result = target;
                }
            }
            return result;
        }
    }
}
