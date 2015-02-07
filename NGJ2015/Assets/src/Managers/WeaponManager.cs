using UnityEngine;
using System.Collections;
using Assets.src.Managers;

namespace Assets.src.Managers
{
    public class WeaponManager : ManagerBase
    {
        public void ClubAttack(Transform transform) {
			iTween.PunchRotation(transform.GetChild (0).GetChild(3).gameObject, new Vector3(0, 0, -120),0.5f);
			iTween.ShakePosition(Camera.main.gameObject, Vector3.one*0.02f, 0.5f);
			var colliders = Physics.OverlapSphere(transform.position + Vector3.right*2.5f, 3f);
			foreach(var collider in colliders)
			{
				iTween.PunchScale(collider.gameObject, Vector3.one*10.1f, 0.5f);
			}
		}
    }
}