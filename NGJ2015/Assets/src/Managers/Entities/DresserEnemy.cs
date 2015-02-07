﻿using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.src.Managers.Entities
{
    public class DresserEnemy : Enemy
	{

        private float _moveCloserDist = 10;
        private float _moveAwayDist = 8;

        private float _nearbyMonstersDist = 15;

        private int _minFramesToKeepAStrategy = 10;

        private int _strategyFrameNum = 0;
        private MonsterStrategy _previousStrategy;

        private int _drawersLeft = 3;

        private enum MonsterStrategy
        {
            MoveCloser,
            Idle,
            MoveAway
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

            _target = GetNearestTarget(_targets, transform.position).GetComponent<CharacterBase>();

            _nearbyMonsters =
                ManagerCollection.Instance.EnemyManager.GetActiveMonsters()
                    .Select(
                        m => new MonsterDist { Dist = (m.transform.position - transform.position), Monster = m })
                    .Where(m => m.Dist.magnitude < _nearbyMonstersDist)
                    .ToList();

            _strategyFrameNum++;
            if (_strategyFrameNum < _minFramesToKeepAStrategy)
            {
                return _previousStrategy;
            }
            _strategyFrameNum = 0;

            var targetDistance = (_target.transform.position - transform.position).magnitude;

            if (targetDistance >= _moveCloserDist || _drawersLeft == 0)
            {
                _previousStrategy = MonsterStrategy.MoveCloser;
                return _previousStrategy;
            }

            if (targetDistance < _moveAwayDist)
            {
                _previousStrategy = MonsterStrategy.MoveAway;
                return _previousStrategy;
            }

            _previousStrategy = MonsterStrategy.Idle;
            return _previousStrategy;
        }

        private void Shoot()
        {
            if (_drawersLeft > 0)
            {
                _drawersLeft--;
                // TODO: Shoot
            }
        }

        private void ExecuteIdleStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.magenta;
            var r = new Random();
            if (_target && r.Next(50) < 2)
            {
                UpdatePosition(Vector3.MoveTowards(transform.position, transform.position + (new Vector3
                {
                    x = (float)(r.NextDouble() - 0.5),
                    y = (float)(r.NextDouble() - 0.5),
                    z = 0
                }), Time.fixedDeltaTime*(_speed/2)));
            }
        }



        private void ExecuteMoveAwayStrategy()
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;


            if (_target != null)
            {
                UpdatePosition(Vector3.MoveTowards(transform.position, transform.position + (transform.position - _target.transform.position), Time.fixedDeltaTime * _speed));
            }
            
        }

        private void ExecuteMoveCloserStrategy()
        {
            //var msg = string.Format("Enemy {0} is spreading towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

            if (_target != null)
            {
                UpdatePosition(Vector3.MoveTowards(transform.position, _target.transform.position, Time.fixedDeltaTime * _speed));
            }
        }

		public override void TakeDamage(float damage) {
			if (_health - damage > 0) {
				ManagerCollection.Instance.AudioManager.PlayAudio(Assets.src.Common.Enumerations.Audio.DresserHit);
			}
			base.TakeDamage (damage);
		}

		public override void Die() {
			base.Die();
			var anim = transform.GetChild(0).GetComponent<Animator> ();
			ManagerCollection.Instance.AudioManager.PlayAudio (Assets.src.Common.Enumerations.Audio.DresserDead);
			anim.SetBool ("isDead",true);
		}
    }
}
