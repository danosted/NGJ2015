﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Enemy : CharacterBase
	{
		private CharacterBase _target;
        private List<GameObject> _targets;

        private List<MonsterDist> _nearbyMonsters;

        public float NearbyMonstersDist = 5;
        public int SwarmThreshold = 5;
        public int SpreadThreshold = 10;
        public int minEnemyDistance = 2;

        public enum MonsterStrategy
        {
            Spread,
            Swarm,
            Attack
        }

		public void SetTargets(List<GameObject> targetObjects)
		{
			_targets = targetObjects;
		}

        public void Update()
        {
        }

        public void FixedUpdate()
        {

		    var strategy = ChooseStrategy();

		    switch (strategy)
		    {
                case MonsterStrategy.Attack:
		            ExecuteAttackStrategy();
                    break;

                case MonsterStrategy.Spread:
                    ExecuteSpreadStrategy();
                    break;

                case MonsterStrategy.Swarm:
                    ExecuteSwarmStrategy();
                    break;

		    }

            transform.position += keepEnemyDistance();

		    if (_target)
		    {
                if (Vector3.Magnitude(transform.position - _target.transform.position) < _range)
                {
                    _target.TakeDamage(_damage);
                }
		    }
		    
		}

        private MonsterStrategy ChooseStrategy()
        {
            _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();
            _nearbyMonsters =
                ManagerCollection.Instance.EnemyManager.GetActiveMonsters()
                    .Select(
                        m => new MonsterDist {Dist = (m.transform.position - transform.position), Monster = m})
                    .Where(m => m.Dist.magnitude < NearbyMonstersDist)
                    .ToList();
            
            
            if (_nearbyMonsters.Count >= SwarmThreshold && _nearbyMonsters.Count < SpreadThreshold)
            {
                return MonsterStrategy.Swarm;
            }

            if (_nearbyMonsters.Count >= SpreadThreshold)
            {
                return MonsterStrategy.Spread;
            }

            return MonsterStrategy.Attack;
        }

        private void ExecuteAttackStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
            if (_target)
            {
                //var msg = string.Format("Enemy {0} is moving towards {1}.", gameObject, _target);
                //Debug.Log(msg, gameObject);
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
            }
        }


        private void ExecuteSpreadStrategy()
        {
            //var msg = string.Format("Enemy {0} is spreading towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

            var centerPosition = new Vector3();

            foreach (var pos in _nearbyMonsters.Select(m => m.Monster.transform.position))
            {
                centerPosition += pos;
            }

            centerPosition /= _nearbyMonsters.Count;

            var direction = transform.position - centerPosition;

            if (_target)
            {
                direction += _target.transform.position - transform.position;
                direction /= 2;
            }

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * _speed);
        }

        private void ExecuteSwarmStrategy()
        {
            //var msg = string.Format("Enemy {0} is swarming towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;

            var finalDirection = new Vector3();
            var positions = _nearbyMonsters.Select(m => m.Monster.transform.position);
            foreach (var pos in positions)
            {
                finalDirection += pos;
            }

            finalDirection /= _nearbyMonsters.Count;
            if (_target)
            {
                finalDirection += _target.transform.position;
                finalDirection /= 2;
            }

            transform.position = Vector3.MoveTowards(transform.position, finalDirection, Time.deltaTime * _speed);
        }

        public override void Die()
        {
            Debug.Log("Enemy die");
            base.Die();
            ManagerCollection.Instance.EnemyManager.PoolEnemyObject(gameObject);
        }

        private GameObject GetNearestTarget(List<GameObject> targets, Vector3 position)
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

        private class MonsterDist
        {
            public Vector3 Dist;
            public GameObject Monster;
        }
        private Vector3 keepEnemyDistance()
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
    }
}
