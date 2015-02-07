using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public abstract class CharacterBase : MonoBehaviour
    {
		private int health;

		public CharacterBase(int health)
		{
			this.health = health;
		}

		protected void Move(Vector3 movement)
		{
			transform.position += movement;
		}
    }
}
