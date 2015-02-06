using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public abstract class CharacterBase
    {
		private int health;
		private Vector2 position;

		public CharacterBase(int health)
		{
			this.health = health;
			this.position = Vector2.zero;
		}

		public abstract void move(Vector2 movement)
		{
			this.position += movement;
		}
    }
}
