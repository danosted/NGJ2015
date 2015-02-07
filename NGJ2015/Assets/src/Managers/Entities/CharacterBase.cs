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
		private int health;
        protected bool isMoving;

		public CharacterBase(int health)
		{
			this.health = health;
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
    }
}
