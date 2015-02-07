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
                ManagerCollection.Instance.AudioManager.PlayAudio(Enumerations.Audio.PlayerAttack);
            }
            else
            {
                ManagerCollection.Instance.AudioManager.PlayAudio(Enumerations.Audio.PlayerAttack);
            }
            foreach (var collider in colliders)
            {

                //iTween.PunchScale(collider.gameObject, Vector3.one * 10.1f, 0.5f);
                var enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(ClubDamage);
                }
                var character = collider.GetComponent<CharacterBase>();
                if (character != null)
                {
                    var mulitiplier = 1;
                    if ((character as Player) != null)
                    {
                        mulitiplier = 3;
                    }
                }

            }
        }

        private void AssaultRifleAttack(Transform targetTransform)
        {
            var bulletGO = ManagerCollection.Instance.WeaponManager.GetNewProjectileFromType(Enumerations.ProjectileTypes.Drawer, transform.position, transform.rotation);
            var bullet = bulletGO.GetComponent(Enumerations.ProjectileTypes.Drawer.ToString()) as Drawer;
            bullet.ShootDrawer(targetTransform.position, targetTransform.GetComponent<Player>());
        }
    }
}
