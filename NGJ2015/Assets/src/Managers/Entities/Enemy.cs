﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Enemy : CharacterBase
	{
		protected CharacterBase _target;
        protected List<GameObject> _targets;

        protected List<MonsterDist> _nearbyMonsters;

        private int minEnemyDistance = 1;
        

		public void SetTargets(List<GameObject> targetObjects)
		{
			_targets = targetObjects;
		}

        public override void Die()
        {
            Debug.Log("Enemy die");
            base.Die();
            ManagerCollection.Instance.EnemyManager.PoolEnemyObject(gameObject);
        }

        protected GameObject GetNearestTarget(List<GameObject> targets, Vector3 position)
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

        protected Vector3 KeepEnemyDistance()
        {
            Vector3 targetPosition = new Vector3(0, 0, 0);
            Vector3 enemyDistance;
            foreach (var otherEnemy in _nearbyMonsters)
            {
                enemyDistance = otherEnemy.Monster.transform.position - transform.position;
                if ((GetInstanceID() != otherEnemy.Monster.GetInstanceID()) && (enemyDistance.magnitude < minEnemyDistance))
                    targetPosition -= enemyDistance * (minEnemyDistance - enemyDistance.magnitude);
            }
            return targetPosition;
        }


        protected class MonsterDist
        {
            public Vector3 Dist;
            public GameObject Monster;
        }
    }
}
