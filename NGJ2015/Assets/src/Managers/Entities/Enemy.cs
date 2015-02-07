using System;
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
            _nearbyMonsters =
                ManagerCollection.Instance.EnemyManager.GetActiveMonsters()
                    .Select(
                        m => new MonsterDist {Dist = (m.transform.position - transform.position), Monster = m})
                    .TakeWhile((m, i) => m.Dist.magnitude < NearbyMonstersDist)
                    .ToList();

            if (_nearbyMonsters.Count > 10)
            {
                return MonsterStrategy.Swarm;
            }

            if (_nearbyMonsters.Count > 30)
            {
                return MonsterStrategy.Swarm;
            }
            return MonsterStrategy.Attack;
        }

        private void ExecuteAttackStrategy()
        {
            _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();
            if (_target)
            {
                //var msg = string.Format("Enemy {0} is moving towards {1}.", gameObject, _target);
                //Debug.Log(msg, gameObject);
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
            }
        }

        private void ExecuteSpreadStrategy()
        {
            var finalDirection = new Vector3();
            var directions = _nearbyMonsters.Select(m => (-1 / m.Dist.magnitude * m.Dist));
            foreach (var direction in directions)
            {
                finalDirection += direction;
            }
            finalDirection.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, transform.position + finalDirection, Time.deltaTime * _speed);
        }

        private void ExecuteSwarmStrategy()
        {
            var finalDirection = new Vector3();
            var directions = _nearbyMonsters.Select(m => (1 / m.Dist.magnitude * m.Dist));
            foreach (var direction in directions)
            {
                finalDirection += direction;
            }

            finalDirection = finalDirection.normalized + ((transform.position - _target.transform.position) / 2).normalized;

            transform.position = Vector3.MoveTowards(transform.position, transform.position + finalDirection, Time.deltaTime * _speed);
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
    }
}
