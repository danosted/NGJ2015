using System.Linq;
using Assets.src.Common;
using UnityEngine;
using Random = System.Random;

namespace Assets.src.Managers.Entities
{
    public class DresserEnemy : Enemy
    {
        private float _meleeCooldown = 1.5f;
        private float _meleeLastHit = 0f;
        private float _meleeRange = 1.5f;

        private float _cooldown = 2.5f;
        private float _lastShot = 0f;
        private float _moveCloserDist = 10;
        private float _moveAwayDist = 8;

        private float _nearbyMonstersDist = 15;

        private int _minFramesToKeepAStrategy = 10;

        private int _strategyFrameNum = 0;
        private MonsterStrategy _previousStrategy;

        private int _drawersLeft = 3;

        private Weapon weapon;

        private enum MonsterStrategy
        {
            MoveCloser,
            Idle,
            MoveAway
        }

		public override void Initialize(float health, float speed, float range, float damage)
        {
            isDead = false;
			_drawersLeft = 3; 
			_health = health;
			_initialhealth = health;
			_speed = speed;
			_range = range;
			_damage = damage;
			_canMove = true;
			_beingPushedBack = false;
			if (!healthbar)
			{
				healthbar = GetComponentInChildren<HealthbarScript>();
			}
            //Debug.LogError(string.Format("{0}{1} starting with {2} hp", GetType(), gameObject.GetInstanceID(), _health));
			//if (!floatingCombatText)
			//{
			//    floatingCombatText = GetComponentInChildren<FloatingCombatText>();
			//}
			healthbar.Init(_health);
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

            UpdatePosition(transform.position + KeepEnemyDistance());

		    if (_target)
            {
                _lastShot += Time.deltaTime;
                _meleeLastHit += Time.deltaTime;
                if (Vector3.Magnitude(transform.position - _target.transform.position) < _range && _lastShot > _cooldown)
                {
                    Shoot();
                    //_target.TakeDamage(_damage);
                    _lastShot = 0f;
                }
                else if (Vector3.Magnitude(transform.position - _target.transform.position) < _meleeRange && _meleeLastHit > _meleeCooldown)
                {
                    _target.TakeDamage(_damage);
                    _meleeLastHit = 0f;
                    PushBack(((transform.position - _target.transform.position).normalized) * 1.1f, 30);
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
                if (weapon == null)
                {
                    weapon = GetComponent<Weapon>();
                }
                if (_target)
                {
                    weapon.Attack(_target.transform, Enumerations.WeaponType.Drawer);
                }
                _drawersLeft--;
				var anim = GetComponentInChildren<Animator>();

				if (_drawersLeft == 2) {
					anim.SetTrigger("2DrawersLeft");
				} else if (_drawersLeft == 1) {
					anim.SetTrigger("1DrawersLeft");
				} else if (_drawersLeft == 0) {
					anim.SetTrigger("0DrawersLeft");
				}
            }
            else
            {
                // TODO: CHARGE!!!
            }
        }

        private void ExecuteIdleStrategy()
        {
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


            if (_target != null)
            {
                UpdatePosition(Vector3.MoveTowards(transform.position, transform.position + (transform.position - _target.transform.position), Time.fixedDeltaTime * _speed));
            }
            
        }

        private void ExecuteMoveCloserStrategy()
        {
            //var msg = string.Format("Enemy {0} is spreading towards {1}.", gameObject, _target);
            //Debug.Log(msg, gameObject);


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
