using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.src.Common
{
    public class Enumerations
    {
        public enum EnemyType
        {
            DresserEnemy,
			ChairEnemy,
			TrashcanEnemy
        }

        public enum PlayerType
        {
            Player,
            MeleePlayer,
            HomingPlayer,
            RangedPlayer
        }

        public enum PowerupType
        {
            SpeedPowerup,
            DamagePowerup
        }

		public enum WeaponType
		{
			Club,
			Drawer
		}
		public enum ProjectileTypes
		{
			Drawer,
            Bottle
		}

		public enum Audio {
			PlayerAttack,
			ChairDead
		}
    }
}
