using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected float _health;
        protected float _initialhealth;
		protected float _speed;
		protected float _range;
        protected float _damage;
        protected HealthbarScript healthbar;
        //private FloatingCombatText floatingCombatText;

		protected bool _canMove;
		protected bool _beingPushedBack;
		protected Vector3 _pushBackVector;
        public const int DefaultPushbackFrames = 10;
		protected int _pushbackFrames = 10;
		protected int _pushbackFrame = 0;
        protected float _pushbackMagnitude = 1f;

        protected bool isDead;
        public bool IsDead()
        {
            return isDead;
        }

		public virtual void Initialize(float health, float speed, float range, float damage)
		{
		    isDead = false;
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
            //if (!floatingCombatText)
            //{
            //    floatingCombatText = GetComponentInChildren<FloatingCombatText>();
            //}
            healthbar.Init(_health);
		}

		protected void StartMoving(Vector3 movement)
		{
		    transform.position += movement * _speed;
		}

        public virtual void TakeDamage(float damage)
        {
            var player = this as Player;

            //Debug.LogError(string.Format("{3}{0} taking {1} damage - {2} hp left {4}", gameObject.GetInstanceID(), damage, _health - damage, GetType(), player == null || !player.IsDead()));
            
            _health -= damage;
            
            if (healthbar && (player == null || !player.IsDead()))
            {
                iTween.PunchScale(gameObject, Vector3.one * 2f, 0.5f);
                healthbar.DamageTaken(damage);
            }
            //if (floatingCombatText)
            //{
            //    floatingCombatText.ShowFloatingText(damage.ToString());
            //}
            if (_health <= 0 && (player == null || !player.IsDead()))
            {
                Die();
            }
        }

        public void FixedUpdate()
        {
            if (_beingPushedBack)
            {

                if (_pushbackFrame > _pushbackFrames)
                {
                    _pushbackFrame = 0;
                    _beingPushedBack = false;
                    _canMove = true;
                }
                else
                {
                    var dest = transform.position + _pushBackVector;
                    if (dest.x > 30)
                    {
                        dest.x = 30;
					}
                    else if (dest.x < -30)
                    {
                        dest.x = -30;
                    }

                    if (dest.y > 13)
                    {
                        dest.y = 13;
                    }
                    else if (dest.y < -11)
                    {
                        dest.y = -11;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, dest,
                        (Time.fixedDeltaTime*
                         (PushbackSpeed(_pushbackFrame, _pushbackFrames) * _pushbackMagnitude)));

                }
                _pushbackFrame++;
            }
        }

        protected virtual float PushbackSpeed(int frame, int maxFrames)
        {
            return _speed + _speed * (((float)maxFrames - frame) / maxFrames);
        }

        public void PushBack(Vector3 destination, int duration = DefaultPushbackFrames, float magnitude = 1f)
        {
            //_canMove = false;
            _beingPushedBack = true;
            _pushBackVector = destination;
            _pushbackFrames = duration;
            _pushbackMagnitude = magnitude;
        }

        protected void UpdatePosition(Vector3 position)
        {
            if (_canMove)
            {
                if (_beingPushedBack)
                {
                    position = (position - transform.position)/5 + transform.position;
                }
                transform.position = position;
            }
        }

        public virtual void Die()
        {
            isDead = true;
            // die animation
            //Debug.LogError("Die "+GetType()+gameObject.GetInstanceID());
        }
		
        protected void StopMoving()
        {
            StopCoroutine("ConstantMovingEnumerator");
        }

		public float GetSpeed()
		{
			return _speed;
		}

		public float GetRange()
		{
			return _range;
		}

		public float GetDamage()
		{
			return _damage;
		}

        public float GetHealth()
        {
            return _health;
        }

        public override bool Equals(object o)
        {
            var cha = o as CharacterBase;
            return cha != null && cha.gameObject.GetInstanceID() != GetInstanceID();
        }
    }
}
