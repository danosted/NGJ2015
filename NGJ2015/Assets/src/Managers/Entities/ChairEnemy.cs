using System.Linq;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class ChairEnemy : Enemy
	{

        private int _minFramesToKeepAStrategy = 30;

        private int _strategyFrameNum = 0;
        private MonsterStrategy _previousStrategy;

        private float _nearbyMonstersDist = 10;
        private float _chargeDist = 10;
        private int _swarmThreshold = 3;
        private int _spreadThreshold = 8;

        public enum MonsterStrategy
        {
            Spread,
            Swarm,
            Attack
        }

        public MonsterStrategy GetStrategy()
        {
            return _previousStrategy;
        }

        public void Update()
        {
        }

        public new void FixedUpdate()
        {
            base.FixedUpdate();

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

            UpdatePosition(transform.position + KeepEnemyDistance());

		    if (_target)
		    {
                if (Vector3.Magnitude(transform.position - _target.transform.position) < _range)
                {
                    _target.TakeDamage(_damage);
                    PushBack(((transform.position - _target.transform.position).normalized)*1.1f, 30);
                }
		    }
		    
		}

        private MonsterStrategy ChooseStrategy()
        {
            if (_strategyFrameNum++ < _minFramesToKeepAStrategy)
            {
                return _previousStrategy;
            }
            _strategyFrameNum = 0;

            _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();

            _nearbyMonsters =
                ManagerCollection.Instance.EnemyManager.GetActiveMonsters()
                    .Select(
                        m => new MonsterDist { Dist = (m.transform.position - transform.position), Monster = m })
                    .Where(m => m.Dist.magnitude < _nearbyMonstersDist)
                    .ToList();
            

            if ((_target.transform.position - transform.position).magnitude < _chargeDist)
            {
                _previousStrategy = MonsterStrategy.Attack;
                return _previousStrategy;
            }

            
            if (_nearbyMonsters.Count >= _swarmThreshold && _nearbyMonsters.Count < _spreadThreshold)
            {
                _previousStrategy = MonsterStrategy.Swarm;
                return _previousStrategy;
            }

            if (_nearbyMonsters.Count >= _spreadThreshold)
            {
                _previousStrategy =  MonsterStrategy.Spread;
                return _previousStrategy;
            }

            _previousStrategy = MonsterStrategy.Attack;
            return _previousStrategy;
        }

        private void ExecuteAttackStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
            if (_target)
            {
                var extraSpeed = 0f;
                var distance = (transform.position - _target.transform.position).magnitude;
                if (distance < 2*_range)
                {
                    extraSpeed = distance;
                }
                UpdatePosition(Vector3.MoveTowards(transform.position, _target.transform.position,
                    Time.fixedDeltaTime * (_speed + extraSpeed)));
            }
        }


        private void ExecuteSpreadStrategy()
        {
            //var msg = string.Format("Enemy {0} is spreading towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

            var centerPosition = new Vector3();
            var numPos = 0;
            foreach (var m in _nearbyMonsters.Select(m => m.Monster))
            {
                var chairEnemy = m.GetComponent<ChairEnemy>();
                if (chairEnemy == null || chairEnemy.GetStrategy() != MonsterStrategy.Attack)
                {
                    numPos++;
                    centerPosition += m.transform.position;
                }
            }
            if (numPos > 0)
            {
                centerPosition /= numPos;   
            }

            var direction = transform.position - centerPosition;

            if (_target)
            {
                direction *= 2;
                direction += _target.transform.position - transform.position;
                direction /= 3;
            }

            UpdatePosition(Vector3.MoveTowards(transform.position, transform.position + direction, Time.fixedDeltaTime * _speed));
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

            UpdatePosition(Vector3.MoveTowards(transform.position, finalDirection, Time.fixedDeltaTime * _speed));
        }

    }
}
