using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.src.Managers;
using Assets.src.Common;

namespace Assets.src.Managers
{
    public class WeaponManager : ManagerBase
    {
		public GameObject GetNewProjectileFromType(Enumerations.ProjectileTypes bulletType, Vector3 startPos, Quaternion startRot)
		{
			Debug.Log(string.Format("Fetching object with type '{0}'.", bulletType));
			if(InactiveObjects.Exists(x => x.GetComponent(bulletType.ToString())))
			{
				Debug.Log(string.Format("Object found in pool."));
				var poolObject = InactiveObjects.Find(x => x.GetComponent(bulletType.ToString()));
				var inactiveGO = GameObject.Instantiate(poolObject, startPos, startRot) as GameObject;
				ActiveObjects.Add(inactiveGO.gameObject);
				inactiveGO.transform.parent = transform;
				inactiveGO.SetActive(true);
				return inactiveGO;
			}
			var GO = PrefabPool.Find(x => x.GetComponent(bulletType.ToString()) != null);
			if (GO == null)
			{
				var msg = string.Format("No object found with type '{0}'.", bulletType);
				Debug.LogError(msg, gameObject);
			}
			var resultGO = GameObject.Instantiate(GO, startPos, startRot) as GameObject;
			ActiveObjects.Add(resultGO.gameObject);
			resultGO.transform.parent = transform;

			return resultGO;
		}
		
		public List<GameObject> GetActiveBullets()
		{
			//Debug.Log(string.Format("Fetching active monster objects."));
			return ActiveObjects;
		}
		
		public void PoolBullets(GameObject enemyGameObject)
		{
			enemyGameObject.SetActive (false);
			ActiveObjects.Remove (enemyGameObject);
			InactiveObjects.Add (enemyGameObject);
		}
	}
}