using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Weapon
	{
		public void Attack(Transform transform, Enumerations.WeaponType weapon) {
			switch (weapon) {
			case Enumerations.WeaponType.Club:
				ClubAttack(transform);
				break;
			case Enumerations.WeaponType.AssaultRifle:
				AssaultRifleAttack(transform);
				break;
			default:
				Debug.LogError ("Not implemented");
				break;
			}
		}
		
		private void ClubAttack(Transform transform) {
			iTween.PunchRotation (transform.GetChild (0).GetChild (3).gameObject, new Vector3 (0, 0, -120), 0.5f);
			iTween.ShakePosition (Camera.main.gameObject, Vector3.one * 0.02f, 0.5f);
			var colliders = Physics.OverlapSphere (transform.position + Vector3.right * 2.5f, 3f);
			foreach (var collider in colliders) {
				iTween.PunchScale (collider.gameObject, Vector3.one * 10.1f, 0.5f);
			}
		}
		
		private void AssaultRifleAttack(Transform transform) {
			ManagerCollection.Instance.WeaponManager.GetNewProjectileFromType (Enumerations.ProjectileTypes.Bullet, transform.position, transform.rotation);
		}
    }
}
