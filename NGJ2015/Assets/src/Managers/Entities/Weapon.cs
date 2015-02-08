using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.src.Common;
using Assets.src.Input;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.src.Managers.Entities
{
    public class Weapon : MonoBehaviour
    {
        public float ClubDamage = 5;
        public float AssaultRifleDamage = 4;
        public Transform hitDebug;

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
            var mouselook = GetComponent<MouseLook2D>();

            //			crossHairs.position = new Vector3(mousepos.x, mousepos.y, 0f);
            Vector3 playerPos = transform.position;
            Vector2 weaponToMouse = mouselook.currentDirection;
            weaponToMouse.Normalize();
            Debug.DrawRay(transform.position, weaponToMouse);
            Debug.Log(hitDebug.position);
            var colliders =
                Physics.OverlapSphere(hitDebug.position, 4f)
                    .ToList();

            if (colliders.Any())
            {
            }
            else
            {
                ManagerCollection.Instance.AudioManager.PlayAudio(Enumerations.Audio.PlayerAttack);
            }
            colliders = colliders.Distinct().ToList();

            //Debug.LogError(string.Join(", ", colliders.Select(c => c.gameObject.ToString() + c.gameObject.GetInstanceID()).ToArray()));
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
                    if (character.transform == transform) return;
                    iTween.PunchScale(collider.gameObject, Vector3.one * 2f, 0.5f);
                    //Debug.LogWarning(string.Format("Pushing {0} back", character.gameObject));
                    var mulitiplier = 2f;
                    var pushbackMagnitude = 3f;
                    if ((character as Player) != null)
                    {
                        mulitiplier = 3f;
                        pushbackMagnitude = 1f;
                    }
                    character.PushBack(
                        ((character.transform.position - playerPos).normalized)*
                        mulitiplier, (int) (CharacterBase.DefaultPushbackFrames*mulitiplier), pushbackMagnitude);

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
            var bulletGO = ManagerCollection.Instance.WeaponManager.GetNewProjectileFromType(Enumerations.ProjectileTypes.Drawer, transform.position, transform.GetChild(0).transform.rotation);
            var bullet = bulletGO.GetComponent(Enumerations.ProjectileTypes.Drawer.ToString()) as Projectile;
            bullet.ShootProjectile(transform.position, targetTransform.GetComponent<Player>());
        }
    }
}
