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
        public float ClubDamage = 5;
        public float AssaultRifleDamage = 4;

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
			iTween.PunchRotation (transform.GetChild (0).GetChild (3).gameObject, new Vector3 (0, 0, -120), 0.2f);
			iTween.ShakePosition (Camera.main.gameObject, Vector3.one * 0.02f, 0.5f);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
            Vector3 playerPos = transform.position;
            Vector2 weaponToMouse = (mousepos - playerPos).normalized;

            var colliders = Physics.OverlapSphere(transform.position + new Vector3(weaponToMouse.x, weaponToMouse.y, 0f) * 2.5f, 3f);
			foreach (var collider in colliders) {
                if (collider.transform != transform) // Dont hit yourself..
			    {
                    iTween.PunchScale(collider.gameObject, Vector3.one * 10.1f, 0.5f);
                    var enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(ClubDamage);
                    }
                    var character = collider.GetComponent<CharacterBase>();
                    if (character != null)
                    {
                        Debug.LogWarning(string.Format("Pushing {0} back", character.gameObject));
                        var mulitiplier = 1;
                        if ((character as Player) != null)
                        {
                            mulitiplier = 3;
                        }
                        character.PushBack(
                            character.transform.position + ((character.transform.position - playerPos).normalized) *
                            mulitiplier);
                    }
			    }
			    
			}
		}
		
		private void AssaultRifleAttack(Transform transform) {
			ManagerCollection.Instance.WeaponManager.GetNewProjectileFromType (Enumerations.ProjectileTypes.Bullet, transform.position, transform.rotation);
		}
    }
}
