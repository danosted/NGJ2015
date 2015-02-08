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
		protected float _speed;
		protected float _range;
        protected float _damage;
        private HealthbarScript healthbar;
        //private FloatingCombatText floatingCombatText;

        private bool _canMove;
        private bool _beingPushedBack;
        private Vector3 _pushBackVector;
        private const int _defaultPushbackFrames = 10;
        private int _pushbackFrames = 10;
        private int _pushbackFrame = 0;

		public void Initialize(float health, float speed, float range, float damage)
		{
			_health = health;
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
            _health -= damage;
            if (healthbar)
            {
                iTween.PunchScale(gameObject, Vector3.one * 2f, 0.5f);
                healthbar.DamageTaken(damage);
            }
            //if (floatingCombatText)
            //{
            //    floatingCombatText.ShowFloatingText(damage.ToString());
            //}
            if (_health <= 0)
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
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + _pushBackVector,
                        (Time.fixedDeltaTime*
                         (PushbackSpeed(_pushbackFrame, _pushbackFrames))));

                }
                _pushbackFrame++;
            }
        }

        protected virtual float PushbackSpeed(int frame, int maxFrames)
        {
            return _speed + _speed * (((float)maxFrames - frame) / maxFrames);
        }

        public void PushBack(Vector3 destination, int duration = _defaultPushbackFrames)
        {
            //_canMove = false;
            _beingPushedBack = true;
            _pushBackVector = destination;
            _pushbackFrames = duration;
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
            // die animation
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
    }
}
