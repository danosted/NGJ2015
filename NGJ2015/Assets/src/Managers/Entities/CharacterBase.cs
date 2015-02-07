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

		public CharacterBase(float health, float speed, float range, float damage)
		{
			_health = health;
			_speed = speed;
			_range = range;
			_damage = damage;
		}

		protected void StartMoving(Vector3 movement)
		{
		    transform.position = transform.position += movement;
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
    }
}
