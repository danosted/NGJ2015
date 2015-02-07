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
        protected bool isMoving;

		public CharacterBase(float health, float speed, float range, float damage)
		{
			_health = health;
			_speed = speed;
			_range = range;
			_damage = damage;
		}

		protected void StartMoving(Vector3 movement)
		{
		    if (isMoving) return;
            StartCoroutine(ConstantMovingEnumerator(movement));
		}
		
        protected void StopMoving()
        {
            if(!isMoving) return;
            isMoving = false;
            StopCoroutine("ConstantMovingEnumerator");
        }

        private IEnumerator ConstantMovingEnumerator(Vector3 movement)
        {
            isMoving = true;
            while (isMoving)
            {
                transform.position += movement;
                yield return null;    
            }
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
