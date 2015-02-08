using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.Common;
using UnityEngine;

namespace Assets.src.Managers.Entities
{
    public class Weapon : MonoBehaviour
    {
        public float ClubDamage = 5;
        public float AssaultRifleDamage = 4;

        public void Attack(Transform transform, Enumerations.WeaponType weapon)
        {
            switch (weapon)
            {
                case Enumerations.WeaponType.Club:
                    ClubAttack(transform);
                    break;
                case Enumerations.WeaponType.Drawer:
                    AssaultRifleAttack(transform);
                    break;
                default:
                    Debug.LogError("Not implemented");
                    break;
            }
        }

        private void ClubAttack(Transform transform)
        {

            Vector3 mousepos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
            Vector3 playerPos = transform.position;
            Vector2 weaponToMouse = (mousepos - playerPos).normalized;

            var colliders =
                Physics.OverlapSphere(transform.position + new Vector3(weaponToMouse.x, weaponToMouse.y, 0f)*2.5f, 3f);
            if (colliders.Count() > 0)
            {
            }
            else
            {
                ManagerCollection.Instance.AudioManager.PlayAudio(Enumerations.Audio.PlayerAttack);
            }
            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    iTween.PunchScale(collider.gameObject, Vector3.one * 2f, 0.5f);
                    enemy.TakeDamage(ClubDamage);
                }
                var character = collider.GetComponent<CharacterBase>();
                if (character != null)
                {
                    iTween.PunchScale(collider.gameObject, Vector3.one * 2f, 0.5f);
                    Debug.LogWarning(string.Format("Pushing {0} back", character.gameObject));
                    var mulitiplier = 1;
                    if ((character as Player) != null)
                    {
                        mulitiplier = 3;
                    }
                    character.PushBack(
                        ((character.transform.position - playerPos).normalized)*
                        mulitiplier);

                }
                var projectile = collider.GetComponent<Projectile>();
                if (projectile != null)
                {
                    ManagerCollection.Instance.WeaponManager.PoolBullets(projectile.gameObject);
                }
            }
        }

        private void AssaultRifleAttack(Transform targetTransform)
        {
            var bulletGO = ManagerCollection.Instance.WeaponManager.GetNewProjectileFromType(Enumerations.ProjectileTypes.Drawer, transform.position, transform.rotation);
            var bullet = bulletGO.GetComponent(Enumerations.ProjectileTypes.Drawer.ToString()) as Projectile;
            bullet.ShootProjectile(targetTransform.position, targetTransform.GetComponent<Player>());
        }
    }
}
