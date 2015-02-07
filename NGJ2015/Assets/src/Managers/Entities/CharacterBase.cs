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
        private Vector3 _pushBackPosition;

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

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (healthbar)
            {
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

                if ((transform.position - _pushBackPosition).magnitude < 0.1)
                {
                    _beingPushedBack = false;
                    _canMove = true;
                }
                transform.position = Vector3.MoveTowards(transform.position, _pushBackPosition,
                    (float) (Time.fixedDeltaTime * (_speed + 0.2*(_pushBackPosition-transform.position).magnitude)));
            }
        }

        public void PushBack(Vector3 destination)
        {
            _canMove = false;
            _beingPushedBack = true;
            _pushBackPosition = destination;
        }

        protected void UpdatePosition(Vector3 position)
        {
            if (_canMove)
            {
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
