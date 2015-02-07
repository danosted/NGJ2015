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

		public void Initialize(float health, float speed, float range, float damage)
		{
			_health = health;
			_speed = speed;
			_range = range;
			_damage = damage;
		    if (!healthbar)
		    {
		        healthbar = GetComponentInChildren<HealthbarScript>();
		    }
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
            if (_health <= 0)
            {
                Die();
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
