using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.src.Managers.Entities
{
    public class DrawerEnemy : Enemy
	{
		
        public float MoveCloserDist = 6;
        public float MoveAwayDist = 4;

        private MonsterStrategy _previousStrategy;

        private enum MonsterStrategy
        {
            MoveCloser,
            Idle,
            MoveAway
        }


        public void Update()
        {
        }

        public void FixedUpdate()
        {

		    var strategy = ChooseStrategy();

		    switch (strategy)
		    {
                case MonsterStrategy.Idle:
		            ExecuteIdleStrategy();
                    break;

                case MonsterStrategy.MoveAway:
                    ExecuteMoveAwayStrategy();
                    break;

                case MonsterStrategy.MoveCloser:
                    ExecuteMoveCloserStrategy();
                    break;

		    }

            transform.position += KeepEnemyDistance();

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
            _strategyFrameNum++;
            if (_strategyFrameNum < MinFramesToKeepAStrategy)
            {
                return _previousStrategy;
            }

            _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();

            _nearbyMonsters =
                ManagerCollection.Instance.EnemyManager.GetActiveMonsters()
                    .Select(
                        m => new MonsterDist { Dist = (m.transform.position - transform.position), Monster = m })
                    .Where(m => m.Dist.magnitude < NearbyMonstersDist)
                    .ToList();

            var targetDistance = (_target.transform.position - transform.position).magnitude;
            
            if (targetDistance >= MoveCloserDist)
            {
                _previousStrategy = MonsterStrategy.MoveCloser;
                return _previousStrategy;
            }

            if (targetDistance < MoveAwayDist)
            {
                _previousStrategy = MonsterStrategy.MoveAway;
                return _previousStrategy;
            }

            _previousStrategy = MonsterStrategy.Idle;
            return _previousStrategy;
        }

        private void ExecuteIdleStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
            if (_target)
            {
                var r = new Random();
                transform.position += (new Vector3
                {
                    x = (float) r.NextDouble(),
                    y = (float) r.NextDouble(),
                    z = 0
                })/(Time.fixedDeltaTime * _speed);
            }
        }


        private void ExecuteMoveAwayStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;


            if (_target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + (transform.position - _target.transform.position), Time.fixedDeltaTime * _speed);
            }
            
        }

        private void ExecuteMoveCloserStrategy()
        {
            //var msg = string.Format("Enemy {0} is spreading towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

            if (_target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + (_target.transform.position - transform.position), Time.fixedDeltaTime * _speed);
            }
        }


        public override void Die()
        {
            Debug.Log("Enemy die");
            base.Die();
            ManagerCollection.Instance.EnemyManager.PoolEnemyObject(gameObject);
        }
    }
}
